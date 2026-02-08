using Glint.Core.Entities;
using Glint.Core.Repositories;

namespace Glint.Infrastructure.Repositories;

public class AssetRepository : IAssetRepository
{
    public Task<Asset> GetAssetUpload(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Asset> CreateAssetUpload(Asset asset)
    {
        throw new NotImplementedException();
    }

    public Task<Asset> UpdateAssetUpload(Asset asset)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAssetUpload(Asset asset)
    {
        throw new NotImplementedException();
    }
}