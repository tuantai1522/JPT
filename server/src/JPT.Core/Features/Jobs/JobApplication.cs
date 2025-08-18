using JPT.Core.Common;

namespace JPT.Core.Features.Jobs;

public sealed class JobApplication : IBaseEntity
{
    public Guid Id { get; init; } = Guid.CreateVersion7();

    public Guid ApplicantId { get; init; }
    
    public Guid JobId { get; init; }

    public long AppliedAt { get; init; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    
    public Guid CvId { get; init; }

    public JobApplicationStatus Status { get; private set; } = JobApplicationStatus.Applied;
    
    private JobApplication()
    {
        
    }

    internal static JobApplication CreateJobApplication(Guid applicantId, Guid jobId, Guid cvId)
    {
        return new JobApplication
        {
            ApplicantId = applicantId,
            JobId = jobId,
            CvId = cvId
        };
    }

    internal void UpdateJobApplicationStatus(JobApplicationStatus status) => Status = status;
}