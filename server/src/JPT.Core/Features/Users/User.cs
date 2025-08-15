using JPT.Core.Common;
using JPT.Core.Features.Jobs;
using File = JPT.Core.Features.Files.File;

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

    public UserRole Role { get; set; } = UserRole.Employer;
    
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

        // Todo: Consider to add in domain event
        if (role == UserRole.JobSeeker)
        {
            _ = Company.CreateCompany(companyName, user.Id, logoId);
        }

        return user;
    }

    public void UpdateUser(string firstName, string? middleName, string? lastName, File? avatar, Company? company, File? logo)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;

        // Delete old avatar and assign new one
        if (avatar != null && avatar.Id != AvatarId)
        {
            avatar.Delete();
            
            AvatarId = avatar.Id;
        }

        // Only employer can update company information
        if (Role == UserRole.Employer)
        {
            // Delete old logo
            if (company != null && logo != null && company.LogoId != logo.Id)
            {
                logo.Delete();
            }

            company?.UpdateCompany(company.Name, company.Description, logo?.Id);
        }
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

    public Result RemoveCv(File cvToRemove)
    {
        var cv = _cvs.FirstOrDefault(cv => cv.CvId == cvToRemove.Id);

        if (cv is null)
        {
            return Result.Failure(UserErrors.CanNotFindCvOfThisUser);
        }

        _cvs.Remove(cv);
            
        // Remove this cv (mark inactive)
        cvToRemove.Delete();
        
        return Result.Success();
    }

}