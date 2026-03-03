using Glint.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Glint.Infrastructure.Media;

public class FfmpegMediaProcessor : IMediaProcessor
{
    private readonly ILogger<FfmpegMediaProcessor> _logger;
    
    public FfmpegMediaProcessor(ILogger<FfmpegMediaProcessor> logger)
    {
        _logger = logger;
    }
    
    public Task ProcessMediaAsync(Guid assetId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Asset with ID: {AssetId} was processed sucessfully", assetId);
        Thread.Sleep(2000);
        return Task.CompletedTask;
    }
}