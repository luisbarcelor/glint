using Glint.Application.Dtos;

namespace Glint.Application.Services;

public interface IAssetService
{
    Task<Guid> UploadAsync(UploadAssetInput input, CancellationToken stoppingToken);
}