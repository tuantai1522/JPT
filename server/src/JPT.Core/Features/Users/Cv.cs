namespace JPT.Core.Features.Users;

public sealed class Cv
{
    public Guid UserId { get; init; }
    
    public Guid CvId { get; init; }

    private Cv()
    {
        
    }

    public static Cv CreateCv(Guid userId, Guid cvId)
    {
        return new Cv
        {
            UserId = userId,
            CvId = cvId,
        };
    }
}