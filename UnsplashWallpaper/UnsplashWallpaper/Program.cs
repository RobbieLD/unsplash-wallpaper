using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UnsplashWallpaper;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) => services
            .Configure<AppConfig>(config.GetSection("Api"))
            .AddScoped<IUnsplash, Unsplash>()
            .AddScoped<IWallpaperManager, WallpaperManager>())
        .Build();

var manager = host.Services.GetService<IWallpaperManager>();
manager!.RefreshWallpapers().Wait();
