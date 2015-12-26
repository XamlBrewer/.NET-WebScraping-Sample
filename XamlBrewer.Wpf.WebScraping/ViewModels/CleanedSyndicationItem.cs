namespace XamlBrewer.Wpf.WebScraping
{
    using Services.WebScraping;
    using System.ServiceModel.Syndication;

    /// <summary>
    /// SyndicationItem with a cleaned up summary.
    /// </summary>
    /// <remarks>For data binding.</remarks>
    public class CleanedSyndicationItem : SyndicationItem
    {
        internal CleanedSyndicationItem(SyndicationItem item)
            : base(item)
        { }

        /// <summary>
        /// Returns the summary as cleaned up by the web scraping service.
        /// </summary>
        public string CleanSummary
        {
            get
            {
                if (this.Summary == null)
                {
                    return this.CleanContent;
                }

                return this.Summary.Text.FlattenToText();
            }
        }

        /// <summary>
        /// Returns the content as cleaned up by the web scraping service.
        /// </summary>
        public string CleanContent
        {
            get
            {
                if (this.Content == null)
                {
                    return string.Empty;
                }

                if (this.Content is TextSyndicationContent)
                {
                    return (this.Content as TextSyndicationContent).Text.FlattenToText();
                }

                return (this.Content as XmlSyndicationContent).ToString();
            }
        }
    }
}
