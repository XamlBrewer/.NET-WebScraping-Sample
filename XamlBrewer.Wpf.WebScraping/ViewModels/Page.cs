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
                        new Page()
                            {
                                Name="The Daily WTF 1",
                                Url="http://thedailywtf.com/articles/sharked",
                                // Advertisement at the end is just part of the text
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.article-body' /></Steps></Script></Scripts>"
                            },
                        new Page()
                            {
                                Name="The Daily WTF 2",
                                Url="http://thedailywtf.com/articles/power-trip",
                                // Advertisement at the end is just part of the text
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.article-body' /></Steps></Script></Scripts>"
                            },
                        new Page()
                            {
                                Name="World News Daily Report 1",
                                Url="http://worldnewsdailyreport.com/activist-spends-one-year-in-freezer-to-denounce-global-warming/",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.single-archive' /><Step Action='remove' Filter='.single-title .single-info .single-cover-art .ssbp-wrap' /></Steps></Script></Scripts>"
                            },
                        new Page()
                            {
                                Name="World News Daily Report 2",
                                Url="http://worldnewsdailyreport.com/utah-man-who-secretly-had-17-wives-arrested-for-polygamy/",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.single-archive' /><Step Action='remove' Filter='.single-title .single-info .single-cover-art .ssbp-wrap' /></Steps></Script></Scripts>"
                            },
                        new Page()
                            {
                                Name="National Report 1",
                                Url="http://nationalreport.net/god-im-too-busy-to-stop-all-the-gun-violence-in-america-try-some-new-laws/",
                                // Some commented-out HTML is showing at the end
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.entry-content' /><Step Action='remove' Filter='#cevhershare #cevhersharex' /></Steps></Script></Scripts>"
                            },
                        new Page()
                            {
                                Name="National Report 2",
                                Url="http://nationalreport.net/worlds-smallest-woman-fly-drone-across-atlantic-ocean/",
                                // Some commented-out HTML is showing at the end
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.entry-content' /><Step Action='remove' Filter='#cevhershare #cevhersharex' /></Steps></Script></Scripts>"
                            },
                        new Page()
                            {
                                Name="National Report Description",
                                Url="http://nationalreport.net/god-im-too-busy-to-stop-all-the-gun-violence-in-america-try-some-new-laws/",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='meta[name=description]' /><Step Action='attr' Filter='content' /></Steps></Script></Scripts>"
                            },
                        new Page()
                            {
                                Name="News Biscuit 1",
                                Url="http://www.newsbiscuit.com/2015/10/19/taxi-drivers-launch-legal-challenge-to-public-transport/",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.entry' /><Step Action='remove' Filter='.sociable #stafBlock #stafLink .posted' /></Steps></Script></Scripts>"
                            },
                        new Page()
                            {
                                Name="News Biscuit 2",
                                Url="http://www.newsbiscuit.com/2015/10/15/man-does-thing-without-being-asked-to-complete-a-survey-about-it/",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.entry' /><Step Action='remove' Filter='.sociable #stafBlock #stafLink .posted' /></Steps></Script></Scripts>"
                            },
                        new Page()
                            {
                                Name="News Biscuit Keywords",
                                Url="http://www.newsbiscuit.com/2015/10/19/taxi-drivers-launch-legal-challenge-to-public-transport/",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='meta[name=keywords]' /><Step Action='attr' Filter='content' /></Steps></Script></Scripts>"
                            },
                        new Page()
                            {
                                Name="The Onion 1",
                                Url="http://www.theonion.com/article/woman-always-thought-she-would-have-more-impressiv-51482",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.content-text' /></Steps></Script></Scripts>"
                            },
                        new Page()
                            {
                                Name="The Onion 2",
                                Url="http://www.theonion.com/article/nation-demands-nasa-stop-holding-press-conferences-51412",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.content-text' /></Steps></Script></Scripts>"
                            },
                        new Page()
                            {
                                Name="Clickhole 1",
                                Url="http://www.clickhole.com/blogpost/problem-flash-why-doesnt-flash-run-italy-pasta-eve-3204",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.article-text' /></Steps></Script></Scripts>"
                            },
                        new Page()
                            {
                                Name="Clickhole 2",
                                Url="http://www.clickhole.com/article/their-finest-moments-6-greatest-triumphs-nasa-hist-3120",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.article-text' /></Steps></Script></Scripts>"
                            },
                        new Page()
                            {
                                Name="Empire News 1",
                                Url="http://empirenews.net/white-christian-teen-arrested-for-wearing-shoes-with-picture-of-clock-to-school/",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.entry-content' /><Step Action='remove' Filter='#top_share_widget' /></Steps></Script></Scripts>"
                            },
                       new Page()
                            {
                                Name="Empire News 2",
                                Url="http://empirenews.net/scientists-prove-plants-flowers-capable-of-feeling-severe-pain/",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.entry-content' /><Step Action='remove' Filter='#top_share_widget' /></Steps></Script></Scripts>"
                            },
                       new Page()
                            {
                                Name="Empire News Author",
                                Url="http://empirenews.net/scientists-prove-plants-flowers-capable-of-feeling-severe-pain/",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='meta[name=shareaholic:article_author_name]' /><Step Action='attr' Filter='content' /></Steps></Script></Scripts>"
                            },
                       new Page()
                            {
                                Name="Your News Wire 1",
                                Url="http://yournewswire.com/report-more-selfie-related-deaths-than-shark-attacks-in-2015/",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.entry' /><Step Action='remove' Filter='.insert-post-ads .abh_box' /></Steps></Script></Scripts>"
                            },
                       new Page()
                            {
                                Name="Your News Wire 2",
                                Url="http://yournewswire.com/huge-container-ship-disappears-in-bermuda-triangle/",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.entry' /><Step Action='remove' Filter='.insert-post-ads .abh_box' /></Steps></Script></Scripts>"
                            },
                       new Page()
                            {
                                Name="The Daily Currant 1",
                                Url="http://dailycurrant.com/2014/12/18/scientists-genetically-engineer-cows-to-produce-orange-juice/",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.entry' /><Step Action='remove' Filter='aside .fbbar' /></Steps></Script></Scripts>"
                            },
                       new Page()
                            {
                                Name="The Daily Currant 2",
                                Url="http://dailycurrant.com/2013/07/01/nasa-finds-message-from-god-on-mars/",
                                Scripts="<Scripts><Script Field='Content'><Steps><Step Action='select' Filter='.entry' /><Step Action='remove' Filter='aside .fbbar' /></Steps></Script></Scripts>"
                            }
                    };
            }
        }
    }
}
