using Glint.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Glint.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAssetService, AssetService>();
    }
}