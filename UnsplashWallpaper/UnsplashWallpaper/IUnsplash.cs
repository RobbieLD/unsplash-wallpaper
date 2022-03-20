using UnsplashWallpaper.Response;

namespace UnsplashWallpaper
{
    public interface IUnsplash
    {
        Task<RandomImage> GetRandomPhotoAsync();
    }
}