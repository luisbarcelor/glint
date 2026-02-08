using Glint.Application.Dtos;

namespace Glint.Application.Services;

public interface IAssetService
{
    Task<Guid> UploadAsync(UploadAssetCommand command);
}