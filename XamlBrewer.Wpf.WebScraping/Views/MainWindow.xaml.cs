﻿using System.Collections.Generic;
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

        public List<Source> Sources
        {
            get { return Source.TestSources; }
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.SourcesList.ItemsSource = this.Sources;
        }

        void Browser_OnLoadCompleted(object sender, NavigationEventArgs e)
        {
            var browser = sender as WebBrowser;

            if (browser == null || browser.Document == null)
                return;

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
            this.Cursor = Cursors.Wait;

            var source = SourcesList.SelectedItem as Source;
            var scripts = WebScraper.DeserializeScripts(source.Scripts);
            this.ScriptText.Text = scripts.First().Documentation;

            var engine = new WebScraper();
            var target = engine.Run(source.Url, source.Scripts);
            this.Text.Text = target.First().Value;

            if (SourceCheckBox.IsChecked.Value)
            {
                try
                {
                    var html = WebScraper.FetchHtml(source.Url);
                    this.SourceBrowser.NavigateToString(html);
                }
                catch (System.Exception)
                {
                    // Ignore.
                    // Test the scraper's exception handling.
                }
            }

            this.Cursor = Cursors.Arrow;
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

        private void CheckBox2_Checked(object sender, RoutedEventArgs e)
        {
            if (this.SourceBrowser2 == null)
            {
                return;
            }

            var box = sender as CheckBox;
            if (box.IsChecked.Value)
            {
                this.SourceBrowser2.Visibility = System.Windows.Visibility.Visible;
                this.BrowserColumn2.Width = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                this.SourceBrowser2.Visibility = System.Windows.Visibility.Collapsed;
                this.BrowserColumn2.Width = new GridLength(0);
            }
        }

        private void FetchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var html = WebScraper.FetchHtml(UrlInput.Text);
                this.SourceBrowser2.NavigateToString(html);
            }
            catch (System.Exception ex)
            {
                this.SourceBrowser2.NavigateToString(ex.Message);
            }
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            var source = SourcesList.SelectedItem as Source;
            var xmlScripts = "<Scripts>" + ScriptInput.Text + "</Scripts>";
            var scripts = WebScraper.DeserializeScripts(xmlScripts);
            this.ScriptText2.Text = scripts.First().Documentation;

            var engine = new WebScraper();
            var target = engine.Run(UrlInput.Text, xmlScripts);
            this.Text2.Text = target.First().Value;

            if (SourceCheckBox2.IsChecked.Value)
            {
                try
                {
                    var html = WebScraper.FetchHtml(UrlInput.Text);
                    this.SourceBrowser2.NavigateToString(html);
                }
                catch (System.Exception)
                {
                    // Ignore.
                    // Test the scraper's exception handling.
                }
            }

            this.Cursor = Cursors.Arrow;
        }
    }
}

