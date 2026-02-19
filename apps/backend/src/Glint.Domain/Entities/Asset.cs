namespace Glint.Domain.Entities;

public class Asset
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string FileName { get; private set; }
    public string OriginalExtension { get; private set; }
    public string TargetFormat { get; private set; }

    public Asset(string fileName, string originalExtension, string targetFormat)
    {
        FileName = fileName;
        OriginalExtension = originalExtension;
        TargetFormat = targetFormat;
    }
}