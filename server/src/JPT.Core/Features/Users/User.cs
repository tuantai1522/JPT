using JPT.Core.Common;
using File = JPT.Core.Features.Files.File;

namespace JPT.Core.Features.Users;

public sealed class User : IDateTracking, IAggregateRoot
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    
    public string FirstName { get; private set; } = null!;
    public string? MiddleName { get; private set; }
    public string? LastName { get; private set; }
    
    public string? Description { get; private set; }

    /// <summary>
    /// Information for login
    /// </summary>
    public string Email { get; private set; } = null!;
    public string HashPassword { get; private set; } = null!;

    public long CreatedAt { get; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    public long? UpdatedAt { get; private set; }

    /// <summary>
    /// Company if this user role is job-seeker
    /// </summary>
    public Company? Company { get; private set; }
    
    public UserRole Role { get; set; } = UserRole.Employee;
    
    public File? Avatar { get; private set; }
    public Guid? AvatarId { get; private set; }

    private User()
    {
        
    }
    
    public static User CreateUser(string firstName, string? middleName, string? lastName, string? description, string email, string hashPassword, Guid avatarId, UserRole role)
    {
        return new User
        {
            FirstName = firstName,
            MiddleName = middleName,
            LastName = lastName,
            Description = description,
            Email = email,
            HashPassword = hashPassword,
            AvatarId = avatarId,
            Role = role,
        };
    }

}