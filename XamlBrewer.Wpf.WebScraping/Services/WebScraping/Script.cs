using System.Collections.Generic;
using System.Xml.Serialization;

namespace XamlBrewer.Services.WebScraping
{
    public class Script
    {
        public Script()
        {
            Steps = new List<Step>();
        }

        [XmlAttribute()]
        public string Field { get; set; }

        public List<Step> Steps { get; set; }

        public string Documentation
        {
            get {
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
