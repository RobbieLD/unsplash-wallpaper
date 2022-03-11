namespace UnsplashWallpaper.Response
{
    public class RandomImage
    {
        public string description { get; set; } = string.Empty;
        public string alt_description { get; set; } = string.Empty;
        public Urls urls { get; set; } = new();
        public Links links { get; set; } = new();
        public Location location { get; set; } = new();
    }
}