using JPT.Core.Common;
using JPT.Core.Features.Jobs;
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

    public long CreatedAt { get; init; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    public long? UpdatedAt { get; private set; }

    /// <summary>
    /// Company if this user role is job-seeker
    /// </summary>
    public Company? Company { get; private set; }
    
    public UserRole Role { get; set; } = UserRole.Employer;
    
    public File? Avatar { get; private set; }
    public Guid? AvatarId { get; private set; }
    
    /// <summary>
    /// List saved jobs of this user.
    /// </summary>
    private readonly List<SavedJob> _savedJobs = [];
    
    public IReadOnlyList<SavedJob> SavedJobs => _savedJobs.ToList();
    
    /// <summary>
    /// List applications of this user.
    /// </summary>
    private readonly List<JobApplication> _jobApplications = [];
    
    public IReadOnlyList<JobApplication> JobApplications => _jobApplications.ToList();
    
    /// <summary>
    /// List cvs of this user.
    /// </summary>
    private readonly List<Cv> _cvs = [];
    
    public IReadOnlyList<Cv> Cvs => _cvs.ToList();

    private User()
    {
        
    }
    
    public static User CreateUser(string firstName, string? middleName, string? lastName, string email, string hashPassword, Guid? avatarId, UserRole role, string companyName, Guid? logoId)
    {
        var user =  new User
        {
            FirstName = firstName,
            MiddleName = middleName,
            LastName = lastName,
            Email = email,
            HashPassword = hashPassword,
            AvatarId = avatarId,
            Role = role,
        };

        if (role == UserRole.JobSeeker)
        {
            user.Company = Company.CreateCompany(companyName, user.Id, logoId);
        }

        return user;
    }

    public void UpdateUser(string firstName, string? middleName, string? lastName, Guid? avatarId, string companyName, string? companyDescription, Guid? logoId)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;

        // Delete old avatar
        if (avatarId != null && Avatar != null && avatarId != AvatarId)
        {
            Avatar.Delete();
        }
        
        AvatarId = avatarId;

        // Only employer can update company information
        if (Role == UserRole.Employer && Company != null)
        {
            // Delete old logo
            if (Company.Logo != null && Company.LogoId != null && Company.LogoId != logoId)
            {
                Company.Logo.Delete();
            }
            
            Company.UpdateCompany(companyName, companyDescription, logoId);
        }
    }

    public Result AddCv(Cv newCv)
    {
        if (Role != UserRole.JobSeeker)
        {
            return Result.Failure(UserErrors.EmployerCanNotAddNewCv);
        }
        
        if (_cvs.Count >= CvLimit.Default.Value)
        {
            return Result.Failure(UserErrors.ExceedMaximumCv(CvLimit.Default.Value));
        }

        if (_cvs.Any(cv => cv.CvId == newCv.CvId))
        {
            return Result.Failure(UserErrors.AlreadyAddThisCv);
        }
        
        _cvs.Add(newCv);

        return Result.Success();
    }

}