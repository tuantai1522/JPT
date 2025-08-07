using JPT.Core.Features.Users;

namespace JPT.Core.Features.Jobs;

public sealed class JobApplication
{
    public Guid UserId { get; init; }
    public User User { get; init; } = null!;
    
    public Guid JobId { get; init; }
    public Job Job { get; init; } = null!;

    public long AppliedAt { get; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

    public JobApplicationStatus Status { get; private set; } = JobApplicationStatus.Applied;
    
    public bool IsDeleted { get; set; }
    public long? DeletedAt { get; set; }

    private JobApplication()
    {
        
    }

    public static JobApplication CreateJobApplication(Guid userId, Guid jobId)
    {
        return new JobApplication
        {
            UserId = userId,
            JobId = jobId,
        };
    }
}