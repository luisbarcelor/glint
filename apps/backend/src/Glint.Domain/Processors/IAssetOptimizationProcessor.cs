using Glint.Domain.Jobs;

namespace Glint.Domain.Processors;

public interface IAssetOptimizationProcessor
{ 
    Task ProcessAsync(AssetOptimizationJob job, CancellationToken stoppingToken);
}