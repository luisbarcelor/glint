using Glint.Application.Services;
using Glint.Core.Repositories;
using Glint.Infrastructure.Repositories;
using Scalar.AspNetCore;

namespace Glint.HttpApi;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        ConfigureServices(builder.Services);
        
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
        
        app.UseHttpsRedirection();
        app.MapControllers();
        
        await app.RunAsync();
    }
    
    // This configures app services
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddControllers();
        services.AddTransient<IAssetService, AssetService>();
        services.AddTransient<IAssetRepository, AssetRepository>();
    }
}