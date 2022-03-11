using Microsoft.Extensions.Options;

namespace UnsplashWallpaper
{
    public class WallpaperManager : IWallpaperManager
    {
        private readonly IUnsplash _unsplash;
        private readonly HttpClient _client = new();
        private readonly AppConfig _appConfig;
        private readonly IImageProcessor _imageProcessor;

        public WallpaperManager(IUnsplash unsplash, IImageProcessor imageProcessor, IOptions<AppConfig> options)
        {
            _unsplash = unsplash;
            _appConfig = options.Value;
            _imageProcessor = imageProcessor;
        }

        public async Task RefreshWallpapers()
        {
            var tasks = new List<Task>();

            for (int i = 0; i < _appConfig.Total; i++)
            {
                tasks.Add(DownloadWallPaper(i));
            }

            await Task.WhenAll(tasks);
        }

        private async Task DownloadWallPaper(int number)
        {
            (string url, string description) = await _unsplash.GetRandomPhotoAsync();
            var response = await _client.GetAsync(url);

            string path = $"{_appConfig.Location}\\{number}.jpg";

            _imageProcessor.AddCaption(await response.Content.ReadAsStreamAsync(), description, path);
        }
    }
}
