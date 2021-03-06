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

        public async Task<RandomImage> GetRandomPhotoAsync()
        {
            string path = $"{_baseUrl}photos/random/?collections={_appConfig.Collection}&orientation={_appConfig.Orientation}&client_id={_appConfig.AccessKey}";
            HttpResponseMessage response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<RandomImage>();
            }
            else
            {
                throw new Exception($"Http Response for {path} was {response.StatusCode}");
            }
        }
    }
}
