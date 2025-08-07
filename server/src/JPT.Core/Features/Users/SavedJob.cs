using JPT.Core.Features.Jobs;

namespace JPT.Core.Features.Users;

public sealed class SavedJob
{
    public Guid ApplicantId { get; init; }
    public User Applicant { get; init; } = null!;
    
    public Guid JobId { get; init; }
    public Job Job { get; init; } = null!;

    private SavedJob()
    {
        
    }

    public static SavedJob CreateSavedJob(Guid applicantId, Guid jobId)
    {
        return new SavedJob
        {
            ApplicantId = applicantId,
            JobId = jobId,
        };
    }
}