using JPT.Core.Common;

namespace JPT.Core.Features.Files;

public class File : IAggregateRoot
{
    public Guid Id { get; init; } = Guid.CreateVersion7();

    public string? Name { get; private set; }

    public string Path { get; private set; } = null!;

    private File()
    {
        
    }

    public static File CreateFile(string? name, string path)
    {
        return new File
        {
            Name = name,
            Path = path
        };
    }
}