using CsQuery;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace XamlBrewer.Services.WebScraping
{
    public class WebScraper
    {
        /// <summary>
        /// Executes a list of web scraping scripts against a URL.
        /// </summary>
        /// <param name="url">The URL to scrape.</param>
        /// <param name="scriptsXml">An XML string containing the list of scripts.</param>
        /// <returns>A dictionary with field names (like 'content' and 'author') and their scraped content.</returns>
        public Dictionary<string, string> Run(string url, string scriptsXml)
        {
            var result = new Dictionary<string, string>();

            var scripts = Script.DeserializeScripts(scriptsXml);

            try
            {
                string html = FetchHtml(url);

                foreach (var script in scripts)
                {
                    var parsed = Eval(script, html);
                    result.Add(script.Field, parsed);
                }
            }
            catch (Exception ex)
            {
                result.Add("Error", ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Fetches the full HTML content of a URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="timeout">Optional timeout (in milliseconds) for the call. Default value is 5000.</param>
        /// <returns>HTML</returns>
        public static string FetchHtml(string url, int timeout = 5000)
        {
            var result = string.Empty;
            string responseCharset = string.Empty;

            // Prepare the request.
            var request = (HttpWebRequest)WebRequest.Create(url);

            // Some sites require cookies.
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;

            request.Timeout = timeout;
            request.Method = "GET";

            // Makes the request look like it comes from a real browser.
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)";

            // These settings did no have any impact:
            // request.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
            // request.AllowAutoRedirect = false;
            // request.Accept = "text/html,application/xhtml+xml,application/xml";
            // request.Headers.Add("Accept-Language", "en-US");
            // request.ContentType = "text/plain";

            MemoryStream stream = new MemoryStream();

            using (var webResponse = (HttpWebResponse)request.GetResponse())
            {
                // Get correct charset and encoding from the server's header
                responseCharset = webResponse.CharacterSet;
                var encoding = Encoding.GetEncoding(responseCharset);

                webResponse.GetResponseStream().CopyTo(stream);
                stream.Position = 0;

                // Read response
                var streamReader = new StreamReader(stream, encoding);
                result = streamReader.ReadToEnd(); // Don't dispose the stream yet, we might need to read it again.
            }

            // Check charset meta-tag in the HTML page.
            var pageCharset = result.GetCharset();

            if (!string.IsNullOrEmpty(pageCharset) && !pageCharset.Equals(responseCharset, StringComparison.InvariantCultureIgnoreCase))
            {
                // Charset meta-tag in HTML differs from supplied server header
                try
                {
                    // Get correct encoding
                    var correctEncoding = Encoding.GetEncoding(pageCharset);

                    // Read the web page again, but with correct encoding this time
                    stream.Position = 0;
                    using (var streamReader = new StreamReader(stream, correctEncoding))
                    {
                        result = streamReader.ReadToEnd();
                    }
                }
                catch (Exception)
                {
                    // Something went wrong (e.g. an invalid charset in the meta tag). 
                    // Ignore the exception, and just keep the first result.
                }
            }
            else
            {
                stream.Dispose();
            }

            return result;
        }

        private string Eval(Script script, string html)
        {
            CQ result = html;

            foreach (var step in script.Steps)
            {
                result = Eval(step, result);
            }

            return result.RenderSelection().FlattenToText();
        }

        private CQ Eval(Step step, CQ html)
        {
            string[] separators = { " " };

            // Multiple filters can be combined with a space
            var filters = step.Filter.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            switch (step.Action)
            {
                case "select":
                    var result = string.Empty;
                    foreach (var filter in filters)
                    {
                        result += html[filter].RenderSelection().Trim() + "\n";
                    }

                    return result;
                case "remove":
                    foreach (var filter in filters)
                    {
                        var selection = html[filter];
                        selection.Remove();
                    }

                    return html;
                case "attr":
                    return html.Attr(filters[0]);
                default:
                    return html;
            }
        }
    }
}
