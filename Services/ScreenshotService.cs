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

            string imagePath = Path.Combine(folder, "screen.png");

            bitmap.Save(imagePath, ImageFormat.Png);

            return imagePath;
        }
    }
}