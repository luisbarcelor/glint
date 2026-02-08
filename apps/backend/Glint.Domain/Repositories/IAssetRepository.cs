using Glint.Core.Entities;

namespace Glint.Core.Repositories;

public interface IAssetRepository
{
    Task<Asset> GetAssetUpload(Guid id);
    Task<Asset> CreateAssetUpload(Asset asset);
    Task<Asset> UpdateAssetUpload(Asset asset);
    Task DeleteAssetUpload(Asset asset);
}