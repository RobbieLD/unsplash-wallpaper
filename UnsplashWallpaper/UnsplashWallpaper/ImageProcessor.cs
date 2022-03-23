using Microsoft.Extensions.Options;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Versioning;
using System.Text;
using UnsplashWallpaper.Response;

namespace UnsplashWallpaper
{
    [SupportedOSPlatform("windows")]
    public class ImageProcessor : IImageProcessor
    {
        private readonly AppConfig _appConfig;

        private const float withScale = 0.05f;
        private const float heightScale = 0.1f;
        private const float fontScale = 0.01f;
        private const int padding = 50;
        private const int transparency = 100;
        private const int cornerRadius = 15;
        public ImageProcessor(IOptions<AppConfig> options)
        {
            _appConfig = options.Value;
        }

        public void AddCaption(Stream content, RandomImage randomImage, string path)
        {
            var caption = new StringBuilder();

            caption.AppendLine(string.IsNullOrEmpty(randomImage.location.city) ? string.Empty : $"City: {randomImage.location.city}");
            caption.AppendLine(string.IsNullOrEmpty(randomImage.location.country) ? string.Empty : $"Country: {randomImage.location.country}");
            caption.AppendLine(string.IsNullOrEmpty(randomImage.description) ? string.Empty : $"Description: {randomImage.description}");

            if (caption.Length == 0) return;

            using Image img = Image.FromStream(content);
            using Graphics graphics = Graphics.FromImage(img);
            using Font arialFont = new(_appConfig.FontFace, fontScale * img.Height);

            var backgroundPen = Color.FromArgb(transparency, 255, 255, 255);

            SizeF stringSize = graphics.MeasureString(caption.ToString(), arialFont);
            var captionStartPoint = new Point((int)(withScale * img.Width) + (padding / 2), (int)(heightScale * img.Height) + (padding / 2));
            var boxStartPoint = new Point((int)(withScale * img.Width), (int)(heightScale * img.Height));
            var boxSize = new Size((int)stringSize.Width + padding, (int)stringSize.Height + padding);

            // Darw a rectangle for the caption
            var backgroundRectangle = new Rectangle(boxStartPoint, boxSize);
            DrawRoundedRectangle(graphics, backgroundRectangle, cornerRadius, new Pen(backgroundPen), backgroundPen);

            // Draw the string
            graphics.DrawString(caption.ToString(), arialFont, Brushes.Black, captionStartPoint);
            graphics.Save();
            
            img.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void DrawRoundedRectangle(Graphics gfx, Rectangle Bounds, int CornerRadius, Pen DrawPen, Color FillColor)
        {
            int strokeOffset = Convert.ToInt32(Math.Ceiling(DrawPen.Width));
            Bounds = Rectangle.Inflate(Bounds, -strokeOffset, -strokeOffset);

            DrawPen.EndCap = DrawPen.StartCap = LineCap.Round;

            GraphicsPath gfxPath = new GraphicsPath();
            gfxPath.AddArc(Bounds.X, Bounds.Y, CornerRadius, CornerRadius, 180, 90);
            gfxPath.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y, CornerRadius, CornerRadius, 270, 90);
            gfxPath.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
            gfxPath.AddArc(Bounds.X, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
            gfxPath.CloseAllFigures();

            gfx.FillPath(new SolidBrush(FillColor), gfxPath);
            gfx.DrawPath(DrawPen, gfxPath);
        }

    }
}
