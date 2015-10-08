using System.Collections.Generic;

namespace XamlBrewer.Wpf.WebScraping
{
    public class Source
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string Scripts { get; set; }

        public static List<Source> TestSources
        {
            get
            {
                return new List<Source>()
                    {
                        new Source()
                            {
                                Name="The Daily WTF 1",
                                Url="http://thedailywtf.com/articles/sharked",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.article-body' /></Steps></Script></Scripts>"
                            },
                        new Source()
                            {
                                Name="The Daily WTF 2",
                                Url="http://thedailywtf.com/articles/power-trip",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.article-body' /></Steps></Script></Scripts>"
                            },
                        new Source()
                            {
                                Name="BBC 1",
                                Url="http://www.bbc.co.uk/news/world-middle-east-34410720#sa-ns_mchannel=rss&ns_source=PublicRSS20-sa",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='div[property=articleBody]&lt;p' /><Step Action='remove' Filter='figure' /></Steps></Script></Scripts>"
                            },
                        new Source()
                            {
                                Name="BBC 2",
                                Url="http://www.bbc.co.uk/news/world-asia-34409343#sa-ns_mchannel=rss&ns_source=PublicRSS20-sa",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='div[property=articleBody]&lt;p' /><Step Action='remove' Filter='figure' /></Steps></Script></Scripts>"
                            },
                      new Source()
                            {
                                Name="CNN 1",
                                Url="http://edition.cnn.com/2015/09/28/us/mars-nasa-announcement/index.html",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.zn-body__paragraph' /></Steps></Script></Scripts>"
                            },
                        new Source()
                            {
                                Name="CNN 2",
                                Url="http://edition.cnn.com/2015/09/28/travel/insider-guide-kyoto/index.html?eref=edition",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.zn-body__paragraph' /></Steps></Script></Scripts>"
                            },
                        new Source()
                            {
                                Name="The New York Times 1",
                                Url="http://www.nytimes.com/2015/10/02/world/asia/kunduz-taliban-afghanistan.html?partner=rss&emc=rss&_r=0",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.story-body-text' /></Steps></Script></Scripts>"
                            },
                        new Source()
                            {
                                Name="The New York Times 2",
                                Url="http://www.nytimes.com/2015/10/01/realestate/manhattan-apartment-prices-near-million-dollar-mark-reports-say.html?partner=rss&emc=rss",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.story-body-text' /></Steps></Script></Scripts>"
                            }
                    };
            }
        }
    }
}
