namespace Glint.HttpApi.Services;

public class VideoUploadService : IVideoUploadService
{
    public async Task<Guid> UploadAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("No file provided.");

        var allowedExtensions = new[] { ".mp4", ".mov", ".avi" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
            throw new ArgumentException("Invalid video file extension.");

        Directory.CreateDirectory("/app/temp");

        var fileId = Guid.NewGuid();
        var filePath = Path.Combine("/app/temp", fileId + extension);

        try
        {
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
        }
        catch (Exception ex)
        {
            throw new IOException("File could not be saved.", ex);
        }

        return fileId;
    }
}