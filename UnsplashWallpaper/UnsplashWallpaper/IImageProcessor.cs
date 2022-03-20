
using UnsplashWallpaper.Response;

namespace UnsplashWallpaper
{
    public interface IImageProcessor
    {
        void AddCaption(Stream content, RandomImage randomImage, string path);
    }
}