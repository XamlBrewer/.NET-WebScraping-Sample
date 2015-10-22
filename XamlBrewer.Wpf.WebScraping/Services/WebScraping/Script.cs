using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XamlBrewer.Services.WebScraping
{
    public class Script
    {
        public Script()
        {
            Steps = new List<Step>();
        }

        /// <summary>
        /// Inflates the list of scripts from an XML string.
        /// </summary>
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

        [XmlAttribute()]
        public string Field { get; set; }

        public List<Step> Steps { get; set; }

        public string Documentation
        {
            get
            {
                string result = Field + ":";

                foreach (var step in Steps)
                {
                    result += " " + step.Action + " " + step.Filter;
                }

                return result;
            }
        }
    }
}
