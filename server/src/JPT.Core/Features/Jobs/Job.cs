using JPT.Core.Common;
using JPT.Core.Features.Countries;

namespace JPT.Core.Features.Jobs;

public sealed class Job : IDateTracking, ISoftDelete
{
    public Guid Id { get; init; } = Guid.CreateVersion7();

    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public string? Requirements { get; private set; }
    
    public decimal? MinSalary { get; private set; }
    public decimal? MaxSalary { get; private set; }

    public JobType JobType { get; private set; }

    public JobStatus Status { get; private set; } = JobStatus.Active;
    
    public int JobCategoryId { get; private set; }
    public JobCategory JobCategory { get; private set; } = null!;
    
    public long CreatedAt { get; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    public long? UpdatedAt { get; private set; }
    
    public bool IsDeleted { get; set; }
    public long? DeletedAt { get; set; }
    
    /// <summary>
    /// Belongs to one city.
    /// </summary>
    public Guid CityId { get; private set; }
    public City City { get; private set; } = null!;

    private Job()
    {
        
    }

    public static Job CreateJob(string title, string? description, string? requirements, decimal? minSalary, decimal? maxSalary, int jobCategoryId, JobType jobType, Guid cityId)
    {
        return new Job
        {
            Title = title,
            Description = description,
            Requirements = requirements,
            MinSalary = minSalary,
            MaxSalary = maxSalary,
            JobType = jobType,
            JobCategoryId = jobCategoryId,
            CityId = cityId,
        };
    }
}