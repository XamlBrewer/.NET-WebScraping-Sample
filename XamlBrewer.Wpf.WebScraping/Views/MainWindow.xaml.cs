using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using XamlBrewer.Services.WebScraping;

namespace XamlBrewer.Wpf.WebScraping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var pages = File.ReadAllText("App.config.pages.json");
            var jsonPages = JsonConvert.DeserializeObject<IEnumerable<Page>>(pages);
            this.SourcesList.ItemsSource = jsonPages;
        }

        void Browser_OnLoadCompleted(object sender, NavigationEventArgs e)
        {
            var browser = sender as WebBrowser;

            if (browser == null || browser.Document == null)
            {
                return;
            }

            // .NET 4.5.2 Syntax
            // if (browser?.Document == null) { return; }
            
            HideScriptErrors(browser, true);
        }

        public void HideScriptErrors(WebBrowser wb, bool Hide)
        {
            FieldInfo fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            object objComWebBrowser = fiComWebBrowser.GetValue(wb);
            if (objComWebBrowser == null) return;
            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { Hide });
        }

        private void SourcesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var source = SourcesList.SelectedItem as Page;
            this.UrlInput.Text = source.Url;
            this.ScriptInput.Text = source.Scripts;

            RunButton_Click(this, null); // Very un-MVVM :-)
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (this.SourceBrowser == null)
            {
                return;
            }

            var box = sender as CheckBox;
            if (box.IsChecked.Value)
            {
                this.SourceBrowser.Visibility = System.Windows.Visibility.Visible;
                this.BrowserColumn.Width = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                this.SourceBrowser.Visibility = System.Windows.Visibility.Collapsed;
                this.BrowserColumn.Width = new GridLength(0);
            }
        }

        private void FetchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var html = WebScraper.FetchHtml(UrlInput.Text);
                this.SourceBrowser.NavigateToString(html);
            }
            catch (System.Exception ex)
            {
                this.SourceBrowser.NavigateToString(ex.Message);
            }
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            var xmlScripts = ScriptInput.Text;
            var scripts = Script.DeserializeScripts(xmlScripts);
            this.ScriptText.Text = scripts.First().Documentation;

            var engine = new WebScraper();
            var target = engine.Run(UrlInput.Text, xmlScripts);
            this.Text.Text = target.First().Value;

            if (SourceCheckBox.IsChecked.Value)
            {
                try
                {
                    var html = WebScraper.FetchHtml(UrlInput.Text);
                    this.SourceBrowser.NavigateToString(html);
                }
                catch (System.Exception ex)
                {
                    this.SourceBrowser.NavigateToString(ex.Message);
                }
            }

            this.Cursor = Cursors.Arrow;
        }
    }
}

