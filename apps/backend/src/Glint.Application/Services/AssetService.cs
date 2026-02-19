using Glint.Application.Dtos;
using Glint.Domain.Jobs;
using Glint.Domain.Processors;
using Hangfire;

namespace Glint.Application.Services;

public class AssetService : IAssetService
{
    private static readonly string[] AllowedExtensions = [".mp4", ".mov", ".avi", ".mkv", ".webm", ".m4v"];
    private const long MaxFileSize = 500 * 1024 * 1024;
    
    private readonly IBackgroundJobClient _jobClient;

    public AssetService(IBackgroundJobClient jobClient)
    {
        _jobClient = jobClient;
    }

    public async Task<Guid> UploadAsync(UploadAssetInput input, CancellationToken stoppingToken)
    {
        if (input == null || input.Length == 0)
            throw new ArgumentException("No file provided.");
        
        if (input.Length > MaxFileSize)
            throw new ArgumentException("File is too large.");
        
        if (!input.ContentType.StartsWith("video/", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Invalid file type.");
        
        var extension = Path.GetExtension(input.FileName).ToLowerInvariant();

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
            
            await input.UploadStream.CopyToAsync(destination);
        }
        catch (Exception ex)
        {
            throw new IOException("File could not be saved.", ex);
        }
        
        _jobClient.Enqueue<IAssetOptimizationProcessor>(x => 
            x.ProcessAsync(new AssetOptimizationJob(fileId), stoppingToken));

        return fileId;
    }
}