namespace UnsplashWallpaper
{
    public class AppConfig
    {
        public string AccessKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Total { get; set; }
        public int FontSize { get; set; }
        public int Collection { get; set; }
        public string FontFace { get; set; } = string.Empty;
        public int CaptionPositionX { get; set; }
        public int CaptionPositionY { get; set; }
        public string Orientation { get; set; } = string.Empty;
    }
}
