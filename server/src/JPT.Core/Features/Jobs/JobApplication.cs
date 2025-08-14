using JPT.Core.Features.Users;
using File = JPT.Core.Features.Files.File;

namespace JPT.Core.Features.Jobs;

public sealed class JobApplication
{
    public Guid Id { get; init; } = Guid.CreateVersion7();

    public Guid ApplicantId { get; init; }
    public User Applicant { get; init; } = null!;
    
    public Guid JobId { get; init; }
    public Job Job { get; init; } = null!;

    public long AppliedAt { get; init; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    
    public Guid CvId { get; init; }
    public File Cv { get; init; } = null!;

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
}