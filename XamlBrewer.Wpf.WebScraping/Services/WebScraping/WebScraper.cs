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

            var scripts = DeserializeScripts(scriptsXml);

            try
            {
                string html = FetchHtml(url);

                foreach (var script in scripts)
                {
                    var parsed = Execute(script, html);
                    result.Add(script.Field, parsed);
                }
            }
            catch (Exception ex)
            {
                result.Add("Error", ex.Message);
            }

            return result;
        }

        public static List<Script> DeserializeScripts(string scriptsXml)
        {
            var scripts = new List<Script>();
            var xmlSerializer = new XmlSerializer(typeof(List<Script>), new XmlRootAttribute("Scripts"));
            using (var stream = new StringReader(scriptsXml))
            {
                scripts = (List<Script>)xmlSerializer.Deserialize(stream);
            }

            return scripts;
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
            string charset = string.Empty;

            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Timeout = timeout;
            request.Method = "GET";
            
            // Some sites require cookies.
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;

            // Makes the request look like it comes from a real browser.
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)";

            // Some other settings I tried, but don't seem to make any difference:
            // request.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
            // request.AllowAutoRedirect = false;
            // request.Accept = "text/html,application/xhtml+xml,application/xml";
            // request.Headers.Add("Accept-Language", "en-US");
            // request.ContentType = "text/plain";

            using (var webResponse = (HttpWebResponse)request.GetResponse())
            {
                // Get correct charset and encoding from the server's header
                charset = webResponse.CharacterSet;
                Debug.WriteLine(string.Format("charset: {0}", charset));
                var encoding = Encoding.GetEncoding(charset);

                // Read response
                using (var streamReader = new StreamReader(webResponse.GetResponseStream(), encoding))
                {
                    result = streamReader.ReadToEnd();
                }
            }

            // Check real charset meta-tag in HTML
            var regex = new Regex("(?<=([<META|<meta])(.*)charset=)([^\"'>]*)");
            var realCharset = regex.Match(result).Value;
            if (!string.IsNullOrEmpty(realCharset))
            {
                // Real charset meta-tag in HTML differs from supplied server header
                if (realCharset.ToUpper() != charset.ToUpper())
                {
                    Debug.WriteLine(string.Format("real charset: {0}", realCharset));

                    try
                    {
                        // Get correct encoding
                        var correctEncoding = Encoding.GetEncoding(realCharset);

                        // Read the web page again, but with correct encoding this time
                        var secondRequest = (HttpWebRequest)WebRequest.Create(url);
                        secondRequest.Timeout = timeout;
                        secondRequest.Method = "GET";
                        secondRequest.ContentType = "text/plain";
                        using (var secondResponse = (HttpWebResponse)secondRequest.GetResponse())
                        {
                            // Read response
                            using (var streamReader = new StreamReader(secondResponse.GetResponseStream(), correctEncoding))
                            {
                                result = streamReader.ReadToEnd();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // Something went wrong (e.g. invalid charset).
                        // We'll keep the result from the initial call.
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Flattens HTML into plain text.
        /// </summary>
        public static string Html2Text(string htmlString)
        {
            // Add a new line before breaks and paragraphs.
            htmlString = htmlString.Replace("<br>", "\n<br>");
            htmlString = htmlString.Replace("<br ", "\n<br ");
            htmlString = htmlString.Replace("<p ", "\n<p ");
            htmlString = htmlString.Replace("<p>", "\n<p>");

            // Remove scripts and styles entirely.
            var regexCss = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            htmlString = regexCss.Replace(htmlString, string.Empty);

            // Remove opening and closing tags.
            htmlString = Regex.Replace(htmlString, "<.*?>", string.Empty);
            htmlString = Regex.Replace(htmlString, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);

            // Decode the remaining.
            htmlString = WebUtility.HtmlDecode(htmlString);

            // Trim the result.
            return htmlString.Trim();
        }

        /// <summary>
        /// Executes a full scraping script against a source.
        /// </summary>
        private string Execute(Script script, string html)
        {
            CQ result = html;

            foreach (var step in script.Steps)
            {
                result = Execute(step, result);
            }

            return Html2Text(result.RenderSelection());
        }

        /// <summary>
        /// Executes a single step of a scraping script against a source.
        /// </summary>
        private CQ Execute(Step step, CQ html)
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
                // Feel free to add extra actions here: regex, printf, ...
                default:
                    return html;
            }
        }
    }
}
