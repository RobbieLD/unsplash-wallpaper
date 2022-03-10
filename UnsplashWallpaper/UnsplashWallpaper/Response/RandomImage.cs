namespace UnsplashWallpaper.Response
{
    public class RandomImage
    {
        public object description { get; set; } = string.Empty;
        public object alt_description { get; set; } = string.Empty;
        public Urls urls { get; set; } = new();
        public Links links { get; set; } = new();
        public Location location { get; set; } = new();
    }
}