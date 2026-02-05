namespace Glint.HttpApi.Services;

public class VideoUploadService : IVideoUploadService
{
    private static readonly string[] AllowedExtensions = [".mp4", ".mov", ".avi", ".mkv", ".webm", ".m4v"];
    private const long MaxFileSize = 500 * 1024 * 1024;

    public async Task<Guid> UploadAsync(IFormFile? file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("No file provided.");
        
        if (file.Length > MaxFileSize)
            throw new ArgumentException("File is too large.");
        
        if (!file.ContentType.StartsWith("video/", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Invalid MIME type.");
        
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!AllowedExtensions.Contains(extension))
            throw new ArgumentException("Invalid video file extension.");

        Directory.CreateDirectory("/app/temp");

        var fileId = Guid.NewGuid();
        var filePath = Path.Combine("/app/temp", fileId + extension);

        try
        {
            await using var stream = new FileStream(
                filePath, 
                FileMode.Create, 
                FileAccess.Write,
                FileShare.None, 
                bufferSize: 81920,
                FileOptions.Asynchronous);
            
            await file.CopyToAsync(stream);
        }
        catch (Exception ex)
        {
            throw new IOException("File could not be saved.", ex);
        }

        return fileId;
    }
}