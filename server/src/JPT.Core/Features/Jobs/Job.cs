using JPT.Core.Common;
using JPT.Core.Features.Countries;
using JPT.Core.Features.Users;

namespace JPT.Core.Features.Jobs;

public sealed class Job : IDateTracking, ISoftDelete
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
    public JobCategory JobCategory { get; private set; } = null!;

    public long CreatedAt { get; init; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    public long? UpdatedAt { get; private set; }
    
    public bool IsDeleted { get; set; }
    public long? DeletedAt { get; set; }
    
    /// <summary>
    /// Belongs to one city.
    /// </summary>
    public int CityId { get; private set; }
    public City City { get; private set; } = null!;
        
    /// <summary>
    /// This post is created by company.
    /// </summary>
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; } = null!;
    
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
}