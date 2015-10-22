namespace XamlBrewer.Services.WebScraping
{
    using System.Net;
    using System.Text.RegularExpressions;
    /// <summary>
    /// Extensions methods for strings that contain HTML.
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// Returns the charset meta tag value of HTML.
        /// </summary>
        public static string GetCharset(this string htmlString)
        {
            var match = Regex.Match(htmlString, "<meta(?!\\s*(?:name|value)\\s*=)[^>]*?charset\\s*=[\\s\"']*([^\\s\"'/>]*)", RegexOptions.IgnoreCase);
            if (match.Groups.Count > 1)
            {
                // Found.
                return match.Groups[1].Value;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Flattens HTML into plain text.
        /// </summary>
        public static string FlattenToText(this string htmlString)
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

            return htmlString.Trim();
        }
    }
}
