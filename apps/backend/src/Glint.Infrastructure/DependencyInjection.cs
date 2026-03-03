using Glint.Application.Interfaces;
using Glint.Domain.Repositories;
using Glint.Infrastructure.BackgroundJobs;
using Glint.Infrastructure.Media;
using Glint.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Glint.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAssetRepository, AssetRepository>();
        
        services.AddTransient<IJobDispatcher, HangfireJobDispatcher>();
        services.AddTransient<IMediaProcessor, FfmpegMediaProcessor>();
    }
}