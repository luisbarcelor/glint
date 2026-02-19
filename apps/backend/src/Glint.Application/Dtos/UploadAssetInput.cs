namespace Glint.Application.Dtos;

public class UploadAssetInput
{
    public required string FileName { get; init; }
    public required string ContentType { get; init; }
    public required long Length { get; init; }
    public required Stream UploadStream { get; init; }
}
