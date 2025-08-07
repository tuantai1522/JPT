using File = JPT.Core.Features.Files.File;

namespace JPT.Core.Features.Users;

public sealed class Cv
{
    public Guid ApplicantId { get; init; }
    
    public Guid CvId { get; init; }
    public File File { get; init; } = null!;

    private Cv()
    {
        
    }

    public static Cv CreateCv(Guid applicantId, Guid cvId)
    {
        return new Cv
        {
            ApplicantId = applicantId,
            CvId = cvId,
        };
    }
}