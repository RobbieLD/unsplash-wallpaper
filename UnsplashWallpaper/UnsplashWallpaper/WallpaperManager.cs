using Microsoft.Extensions.Options;

namespace UnsplashWallpaper
{
    public class WallpaperManager : IWallpaperManager
    {
        private readonly IUnsplash _unsplash;
        private readonly HttpClient _client = new();
        private readonly AppConfig _appConfig;

        public WallpaperManager(IUnsplash unsplash, IOptions<AppConfig> options)
        {
            _unsplash = unsplash;
            _appConfig = options.Value;
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
            string url = await _unsplash.GetRandomPhotoAsync();
            var response = await _client.GetAsync(url);

            using var fs = new FileStream($"{_appConfig.Location}\\{number}.jpg", FileMode.Create);
            await response.Content.CopyToAsync(fs);
        }
    }
}
