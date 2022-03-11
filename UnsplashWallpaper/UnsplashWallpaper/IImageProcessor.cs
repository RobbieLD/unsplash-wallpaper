
namespace UnsplashWallpaper
{
    public interface IImageProcessor
    {
        void AddCaption(Stream content, string caption, string path);
    }
}