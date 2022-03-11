namespace UnsplashWallpaper
{
    public interface IUnsplash
    {
        Task<(string url, string description)> GetRandomPhotoAsync();
    }
}