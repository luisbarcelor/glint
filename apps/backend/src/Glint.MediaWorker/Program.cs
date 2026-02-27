using Glint.Domain.Processors;
using Glint.Infrastructure.Processors;
using Hangfire;
using Hangfire.Redis.StackExchange;

namespace Glint.MediaWorker;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        
        ConfigureServices(builder.Services, builder.Configuration);

        var host = builder.Build();
        await host.RunAsync();
    }
    
    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseRedisStorage(configuration.GetConnectionString("Redis"))
        );
        services.AddHangfireServer();
        
        services.AddScoped<IAssetOptimizationProcessor, AssetOptimizationProcessor>();
    }
}