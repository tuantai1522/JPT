using JPT.Core.Common;

namespace JPT.Core.Features.Jobs;

public sealed class Job : IBaseEntity, IDateTracking, ISoftDelete
{
    public Guid Id { get; init; } = Guid.CreateVersion7();

    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public string? Requirements { get; private set; }
    
    public decimal? MinSalary { get; private set; }
    public decimal? MaxSalary { get; private set; }

    public JobType Type { get; private set; }

    public JobStatus Status { get; private set; } = JobStatus.Active;
    
    public int JobCategoryId { get; private set; }

    public long CreatedAt { get; init; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    public long? UpdatedAt { get; private set; }
    
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

    /// <summary>
    /// Belongs to one city.
    /// </summary>
    public int CityId { get; private set; }
        
    /// <summary>
    /// This post is created by company.
    /// </summary>
    public Guid CompanyId { get; private set; }
    
    /// <summary>
    /// List applications of this user.
    /// </summary>
    private readonly List<JobApplication> _jobApplications = [];
    
    public IReadOnlyList<JobApplication> JobApplications => _jobApplications.ToList();

    private Job()
    {
        
    }

    public static Job CreateJob(string title, string? description, string? requirements, decimal? minSalary, decimal? maxSalary, int jobCategoryId, Guid companyId, JobType type, int cityId)
    {
        return new Job
        {
            Title = title,
            Description = description,
            Requirements = requirements,
            MinSalary = minSalary,
            MaxSalary = maxSalary,
            Type = type,
            JobCategoryId = jobCategoryId,
            CompanyId = companyId,
            CityId = cityId,
        };
    }
    
    public void UpdateJob(string title, string? description, string? requirements, decimal? minSalary, decimal? maxSalary, int jobCategoryId, JobType type, int cityId)
    {
        Title = title;
        Description = description;
        Requirements = requirements;
        MaxSalary = maxSalary;
        MinSalary = minSalary;
        JobCategoryId = jobCategoryId;
        Type = type;
        CityId = cityId;
        
        UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
}