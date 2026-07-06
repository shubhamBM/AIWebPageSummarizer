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
using Microsoft.Web.WebView2.Core;
using System.Windows.Forms;

namespace AIWebPageSummarizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private async Task CaptureSummaryPreviewAsync()
        {
            string folder = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Screenshots");

            Directory.CreateDirectory(folder);

            string fileName = $"Summary_{DateTime.Now:yyyyMMdd_HHmmss_fff}.png";

            string imagePath = Path.Combine(folder, fileName);

            using FileStream stream = new FileStream(
                imagePath,
                FileMode.Create,
                FileAccess.Write);

            await webView.CoreWebView2.CapturePreviewAsync(
                CoreWebView2CapturePreviewImageFormat.Png,
                stream);
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

                if (message == "summaryRendered")
                {
                    System.Windows.MessageBox.Show("C# received summaryRendered");

                    await CaptureSummaryPreviewAsync();

                    return;
                }

                if (message != "summarize")
                    return;

                await webView.CoreWebView2.ExecuteScriptAsync("showLoading();");

                ScreenshotService screenshotService = new ScreenshotService();

                string originalImagePath = screenshotService.CaptureScreen();

                AIService aiService = new AIService();

                string summary = await aiService.SummarizeImageAsync(originalImagePath);

                string jsonSummary = System.Text.Json.JsonSerializer.Serialize(summary);

                await webView.CoreWebView2.ExecuteScriptAsync(
                    $"showSummary({jsonSummary});");
            }
            catch (Exception ex)
            {
                string jsonError = System.Text.Json.JsonSerializer.Serialize(ex.ToString());

                await webView.CoreWebView2.ExecuteScriptAsync(
                    $"showError({jsonError});");
            }
        }
    }
    }