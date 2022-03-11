using Microsoft.Extensions.Options;
using System.Drawing;

namespace UnsplashWallpaper
{
    public class ImageProcessor : IImageProcessor
    {
        private readonly AppConfig _appConfig;

        public ImageProcessor(IOptions<AppConfig> options)
        {
            _appConfig = options.Value;
        }

        public void AddCaption(Stream content, string caption, string path)
        {
            using Image img = Image.FromStream(content);
            using Graphics graphics = Graphics.FromImage(img);

            if (!string.IsNullOrEmpty(caption))
            {
                using Font arialFont = new(_appConfig.FontFace, _appConfig.FontSize);
                graphics.DrawString(caption, arialFont, Brushes.Black, new PointF(_appConfig.CaptionPositionX, _appConfig.CaptionPositionY));
                graphics.Save();
            }

            img.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}
