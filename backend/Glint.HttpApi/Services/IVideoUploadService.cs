namespace Glint.HttpApi.Services;

public interface IVideoUploadService
{
    Task<Guid> UploadAsync(IFormFile? file);
}