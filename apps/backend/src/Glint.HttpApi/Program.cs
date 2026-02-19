using Glint.Application.Services;
using Glint.Domain.Repositories;
using Glint.HttpApi.Hangfire;
using Glint.Infrastructure.Repositories;
using Hangfire;
using Hangfire.Redis.StackExchange;
using Scalar.AspNetCore;

namespace Glint.HttpApi;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = [new HangfireAuthFilter()]
            });
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        await app.RunAsync();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config => config
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseRedisStorage(configuration.GetConnectionString("Redis")));

        services.AddOpenApi();
        services.AddControllers();

        services.AddTransient<IAssetService, AssetService>();
        services.AddTransient<IAssetRepository, AssetRepository>();
    }
}