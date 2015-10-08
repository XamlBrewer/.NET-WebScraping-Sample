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
                                // Advertisement at the end is just part of the text
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.article-body' /></Steps></Script></Scripts>"
                            },
                        new Source()
                            {
                                Name="The Daily WTF 2",
                                Url="http://thedailywtf.com/articles/power-trip",
                                // Advertisement at the end is just part of the text
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.article-body' /></Steps></Script></Scripts>"
                            },
                        new Source()
                            {
                                Name="World News Daily Report 1",
                                Url="http://worldnewsdailyreport.com/activist-spends-one-year-in-freezer-to-denounce-global-warming/",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.single-archive' /><Step Action='remove' Filter='.single-title .single-info .single-cover-art .ssbp-wrap' /></Steps></Script></Scripts>"
                            },
                        new Source()
                            {
                                Name="World News Daily Report 2",
                                Url="http://worldnewsdailyreport.com/utah-man-who-secretly-had-17-wives-arrested-for-polygamy/",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.single-archive' /><Step Action='remove' Filter='.single-title .single-info .single-cover-art .ssbp-wrap' /></Steps></Script></Scripts>"
                            },
                        new Source()
                            {
                                Name="National Report 1",
                                Url="http://nationalreport.net/god-im-too-busy-to-stop-all-the-gun-violence-in-america-try-some-new-laws/",
                                // Some commented-out HTML is showing at the end
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.entry-content' /><Step Action='remove' Filter='#cevhershare #cevhersharex' /></Steps></Script></Scripts>"
                            },
                        new Source()
                            {
                                Name="National Report 2",
                                Url="http://nationalreport.net/worlds-smallest-woman-fly-drone-across-atlantic-ocean/",
                                // Some commented-out HTML is showing at the end
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.entry-content' /><Step Action='remove' Filter='#cevhershare #cevhersharex' /></Steps></Script></Scripts>"
                            },
                        new Source()
                            {
                                Name="The Onion 1",
                                Url="http://www.theonion.com/article/woman-always-thought-she-would-have-more-impressiv-51482",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.content-text' /></Steps></Script></Scripts>"
                            },
                        new Source()
                            {
                                Name="The Onion 2",
                                Url="http://www.theonion.com/article/nation-demands-nasa-stop-holding-press-conferences-51412",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.content-text' /></Steps></Script></Scripts>"
                            },
                        new Source()
                            {
                                Name="Clickhole 1",
                                Url="http://www.clickhole.com/blogpost/problem-flash-why-doesnt-flash-run-italy-pasta-eve-3204",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.article-text' /></Steps></Script></Scripts>"
                            },
                        new Source()
                            {
                                Name="Clickhole 2",
                                Url="http://www.clickhole.com/article/their-finest-moments-6-greatest-triumphs-nasa-hist-3120",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.article-text' /></Steps></Script></Scripts>"
                            },
                        new Source()
                            {
                                Name="Empire News 1",
                                Url="http://empirenews.net/white-christian-teen-arrested-for-wearing-shoes-with-picture-of-clock-to-school/",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.entry-content' /><Step Action='remove' Filter='#top_share_widget' /></Steps></Script></Scripts>"
                            },
                       new Source()
                            {
                                Name="Empire News 2",
                                Url="http://empirenews.net/scientists-prove-plants-flowers-capable-of-feeling-severe-pain/",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.entry-content' /><Step Action='remove' Filter='#top_share_widget' /></Steps></Script></Scripts>"
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
