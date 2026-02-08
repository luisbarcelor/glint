using Glint.Application.Dtos;

namespace Glint.Application.Services;

public class AssetService : IAssetService
{
    private static readonly string[] AllowedExtensions = [".mp4", ".mov", ".avi", ".mkv", ".webm", ".m4v"];
    private const long MaxFileSize = 500 * 1024 * 1024;

    public async Task<Guid> UploadAsync(UploadAssetCommand command)
    {
        if (command == null || command.Length == 0)
            throw new ArgumentException("No file provided.");
        
        if (command.Length > MaxFileSize)
            throw new ArgumentException("File is too large.");
        
        if (!command.ContentType.StartsWith("video/", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Invalid file type.");
        
        var extension = Path.GetExtension(command.FileName).ToLowerInvariant();

        if (!AllowedExtensions.Contains(extension))
            throw new ArgumentException("Unsupported video file extension.");

        Directory.CreateDirectory("/glint/temp");

        var fileId = Guid.NewGuid();
        var filePath = Path.Combine("/glint/temp", fileId + extension);

        try
        {
            await using var destination = new FileStream(
                filePath, 
                FileMode.Create, 
                FileAccess.Write,
                FileShare.None, 
                bufferSize: 81920,
                FileOptions.Asynchronous);
            
            await command.UploadStream.CopyToAsync(destination);
        }
        catch (Exception ex)
        {
            throw new IOException("File could not be saved.", ex);
        }

        return fileId;
    }
}