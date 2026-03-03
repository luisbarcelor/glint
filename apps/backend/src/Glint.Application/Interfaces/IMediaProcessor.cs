namespace Glint.Application.Interfaces;

public interface IMediaProcessor
{
    Task ProcessMediaAsync(Guid assetId, CancellationToken cancellationToken);
}