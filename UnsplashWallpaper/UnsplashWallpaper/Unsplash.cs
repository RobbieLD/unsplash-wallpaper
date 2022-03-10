using Microsoft.Extensions.Options;
using UnsplashWallpaper.Response;

namespace UnsplashWallpaper
{
    public class Unsplash : IUnsplash
    {
        private readonly HttpClient _client = new();
        private readonly AppConfig _appConfig;
        private const string _baseUrl = "https://api.unsplash.com/";

        public Unsplash(IOptions<AppConfig> options)
        {
            _appConfig = options.Value;
        }

        public async Task<string> GetRandomPhotoAsync()
        {
            string path = $"{_baseUrl}photos/random/?client_id={_appConfig.AccessKey}";
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                var randomImage = await response.Content.ReadAsAsync<RandomImage>();

                return randomImage.links.download;
            }
            else
            {
                throw new Exception($"Http Response for {path} was {response.StatusCode}");
            }
        }
    }
}
