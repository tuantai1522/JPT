using JPT.Core.Features.Jobs;

namespace JPT.Core.Features.Users;

public sealed class SavedJob
{
    public Guid UserId { get; init; }
    public User User { get; init; } = null!;
    
    public Guid JobId { get; init; }
    public Job Job { get; init; } = null!;

    private SavedJob()
    {
        
    }

    public static SavedJob CreateSavedJob(Guid userId, Guid jobId)
    {
        return new SavedJob
        {
            UserId = userId,
            JobId = jobId,
        };
    }
}