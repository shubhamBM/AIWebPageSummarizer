using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System;
using System.IO;
using AIWebPageSummarizer.Services;

namespace AIWebPageSummarizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }


        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await webView.EnsureCoreWebView2Async();

            webView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;

            string htmlPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Assets",
            "index.html");

            webView.Source = new Uri(htmlPath);
        }

        private async void CoreWebView2_WebMessageReceived(
    object? sender,
    Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                string message = e.TryGetWebMessageAsString();

                if (message != "summarize")
                    return;

               
                await webView.CoreWebView2.ExecuteScriptAsync("showLoading();");

                
                await Task.Delay(100);

                
                ScreenshotService screenshotService = new ScreenshotService();
                string imagePath = screenshotService.CaptureScreen();

               
                AIService aiService = new AIService();
                string summary = await aiService.SummarizeImageAsync(imagePath);

               
                string jsonSummary = System.Text.Json.JsonSerializer.Serialize(summary);

               
                await webView.CoreWebView2.ExecuteScriptAsync(
                    $"showSummary({jsonSummary});");
            }
            catch (Exception ex)
            {
                string jsonError = System.Text.Json.JsonSerializer.Serialize(ex.Message);

                await webView.CoreWebView2.ExecuteScriptAsync(
                    $"showError({jsonError});");
            }
        }
    }
}