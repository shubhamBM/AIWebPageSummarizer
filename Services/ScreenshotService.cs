using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace AIWebPageSummarizer.Services
{
    public class ScreenshotService
    {
        public string CaptureScreen()
        {
            var bounds = Screen.PrimaryScreen.Bounds;

            using Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);

            using Graphics graphics = Graphics.FromImage(bitmap);

            graphics.CopyFromScreen(
                bounds.Left,
                bounds.Top,
                0,
                0,
                bounds.Size);

            string folder = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Screenshots");

            Directory.CreateDirectory(folder);

            string fileName = $"Screen_{DateTime.Now:yyyyMMdd_HHmmss_fff}.png";

            string imagePath = Path.Combine(folder, fileName);

            bitmap.Save(imagePath, ImageFormat.Png);

            return imagePath;
        }
    }
}