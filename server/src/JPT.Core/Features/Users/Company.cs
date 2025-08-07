using File = JPT.Core.Features.Files.File;

namespace JPT.Core.Features.Users;

public sealed class Company
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    
    public string Name { get; private set; } = null!;
    
    public string? Description { get; private set; }
    
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    
    public Guid? LogoId { get; private set; }
    public File? Logo { get; private set; }

    public static Company CreateCompany(string name, string? description, Guid userId, Guid? logoId)
    {
        return new Company
        {
            Name = name,
            Description = description,
            UserId = userId,
            LogoId = logoId,
        };
    }

}