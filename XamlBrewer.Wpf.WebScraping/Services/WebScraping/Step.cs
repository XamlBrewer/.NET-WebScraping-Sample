using System.Xml.Serialization;

namespace XamlBrewer.Services.WebScraping
{
    public class Step
    {
        [XmlAttribute()]
        public string Action { get; set; }

        [XmlAttribute()]
        public string Filter { get; set; }
    }
}
