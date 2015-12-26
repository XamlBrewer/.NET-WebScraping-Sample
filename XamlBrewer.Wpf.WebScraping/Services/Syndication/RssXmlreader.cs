
namespace XamlBrewer.Services.Syndication
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Xml;

    public class RssXmlReader : XmlTextReader
    {
        private bool readingDate = false;

        // Add more date fields here, if required.
        private static List<string> dateFields = new List<string>() { "lastBuildDate", "pubDate" };

        public RssXmlReader(Stream s) : base(s) { }

        public RssXmlReader(string inputUri) : base(inputUri) { }

        public override void ReadStartElement()
        {
            if (string.Equals(base.NamespaceURI, string.Empty, StringComparison.InvariantCultureIgnoreCase) &&
                (dateFields.Find(s => s.IndexOf(base.LocalName, StringComparison.InvariantCultureIgnoreCase) >= 0) != null))
            {
                readingDate = true;
            }

            base.ReadStartElement();
        }

        public override void ReadEndElement()
        {
            if (readingDate)
            {
                readingDate = false;
            }

            base.ReadEndElement();
        }

        public override string ReadString()
        {
            if (readingDate)
            {
                string dateString = base.ReadString();

                DateTimeOffset dt;

                // Try to parse the date string, as is.
                if (!DateTimeOffset.TryParse(dateString, out dt))
                {
                    // Most probable cause of error is an RFC822 date with timezone name instead of offset.
                    dt = dt.FromRfc822String(dateString);
                }

                // Format for the RSS XML reader.
                var result = dt.ToString("R", CultureInfo.InvariantCulture);

                return result;
            }
            else
            {
                return base.ReadString();
            }
        }
    }
}
