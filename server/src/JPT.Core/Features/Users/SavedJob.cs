namespace JPT.Core.Features.Users;

public sealed class SavedJob
{
    public Guid ApplicantId { get; init; }
    
    public Guid JobId { get; init; }

    private SavedJob()
    {
        
    }

    internal static SavedJob CreateSavedJob(Guid applicantId, Guid jobId)
    {
        return new SavedJob
        {
            ApplicantId = applicantId,
            JobId = jobId,
        };
    }
}