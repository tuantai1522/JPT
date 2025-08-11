using JPT.Core.Common;

namespace JPT.Core.Features.Files;

public class File : IAggregateRoot, ISoftDelete
{
    public Guid Id { get; init; } = Guid.CreateVersion7();

    public string? Name { get; private set; }

    public string Path { get; private set; } = null!;

    public long UploadedAt { get; private set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

    public UploadFileType UploadType { get; private set; } = UploadFileType.LocalHost;

    public string MimeType { get; private set; } = null!;
    
    private File()
    {
        
    }

    public static File CreateFile(string? name, string path, UploadFileType uploadType, string mimeType)
    {
        return new File
        {
            Name = name,
            Path = path,
            MimeType = mimeType,
            UploadType = uploadType
        };
    }

    public bool IsDeleted { get; private set; }
    public long? DeletedAt { get; private set; }

    public void Delete()
    {
        if (!IsDeleted)
        {
            IsDeleted = true;
            DeletedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
    }
}