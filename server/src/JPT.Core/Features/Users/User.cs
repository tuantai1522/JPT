using JPT.Core.Common;
using JPT.Core.Features.Files;
using JPT.Core.Features.Jobs;

namespace JPT.Core.Features.Users;

public sealed class User : AggregateRoot, IDateTracking 
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

    public UserRole Role { get; private set; } = UserRole.Employer;
    
    public Guid? AvatarId { get; private set; }
    
    /// <summary>
    /// User has one Company
    /// </summary>
    public Company? Company { get; private set; }
    
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

    public static User CreateUser(string firstName, string? middleName, string? lastName, string email,
        string hashPassword, Guid? avatarId, UserRole role, string companyName, Guid? logoId)
    {
        var user = new User
        {
            FirstName = firstName,
            MiddleName = middleName,
            LastName = lastName,
            Email = email,
            HashPassword = hashPassword,
            AvatarId = avatarId,
            Role = role,
        };

        if (role == UserRole.Employer)
        {
            user.Company = Company.CreateCompany(companyName, user.Id, logoId);
        }

        return user;
    }

    public Result UpdateUser(string firstName, string? middleName, string? lastName, Guid? avatarId, string? description, string companyName,
        string? companyDescription, Guid? logoId)
    {
        if (Role == UserRole.Employer && string.IsNullOrEmpty(companyName))
        {
            return Result.Failure(UserErrors.CompanyNameRequired);
        }

        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Description = description;

        // Delete old avatar and assign new one
        if (AvatarId.HasValue && avatarId != AvatarId)
        {
            AddDomainEvent(new FileDeletedDomainEvent(AvatarId.Value));

            AvatarId = avatarId;
        }

        // Only employer can update company information
        if (Role == UserRole.Employer)
        {
            // Delete old logo
            if (Company is { LogoId: not null } && Company.LogoId != logoId)
            {
                AddDomainEvent(new FileDeletedDomainEvent(Company.LogoId.Value));
            }

            Company?.UpdateCompany(companyName, companyDescription, logoId);
        }
        
        return Result.Success();
    }

    public Result AddCv(Guid cvToAddId)
    {
        if (Role != UserRole.JobSeeker)
        {
            return Result.Failure(UserErrors.EmployerCanNotAddNewCv);
        }
        
        if (_cvs.Count >= CvLimit.Default.Value)
        {
            return Result.Failure(UserErrors.ExceedMaximumCv(CvLimit.Default.Value));
        }

        var cv = _cvs.FirstOrDefault(cv => cv.CvId == cvToAddId);
        if (cv is not null)
        {
            return Result.Failure(UserErrors.AlreadyAddThisCv);
        }
        
        var newCv = Cv.CreateCv(Id, cvToAddId);

        _cvs.Add(newCv);

        return Result.Success();
    }

    public Result RemoveCv(Guid cvToRemoveId)
    {
        var cv = _cvs.FirstOrDefault(cv => cv.CvId == cvToRemoveId);

        if (cv is null)
        {
            return Result.Failure(UserErrors.CanNotFindCvOfThisUser);
        }

        _cvs.Remove(cv);
            
        // Remove this cv (mark inactive)
        AddDomainEvent(new FileDeletedDomainEvent(cvToRemoveId));

        return Result.Success();
    }

}