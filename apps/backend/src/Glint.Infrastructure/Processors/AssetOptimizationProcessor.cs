using Glint.Domain.Jobs;
using Glint.Domain.Processors;
using Microsoft.Extensions.Logging;

namespace Glint.Infrastructure.Processors;

public class AssetOptimizationProcessor : IAssetOptimizationProcessor
{
    private readonly ILogger<AssetOptimizationProcessor> _logger;
    
    public AssetOptimizationProcessor(ILogger<AssetOptimizationProcessor> logger)
    {
        _logger = logger;
    }
    
    public async Task ProcessAsync(AssetOptimizationJob job, CancellationToken stoppingToken)
    {
        _logger.LogInformation("Processing asset {AssetId}", job.id);
        await Task.CompletedTask;
    }
}