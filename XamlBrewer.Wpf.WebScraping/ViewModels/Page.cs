using System.Collections.Generic;

namespace XamlBrewer.Wpf.WebScraping
{
    public class Page
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string Scripts { get; set; }

        public static List<Page> TestSources
        {
            get
            {
                return new List<Page>()
                    {

                    };
            }
        }
    }
}
