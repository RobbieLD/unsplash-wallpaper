namespace UnsplashWallpaper
{
    public interface IUnsplash
    {
        Task<string> GetRandomPhotoAsync();
    }
}