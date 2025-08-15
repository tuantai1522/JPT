using JPT.Core.Common;

namespace JPT.Core.Features.Users;

public sealed class Cv : IBaseEntity
{
    public Guid ApplicantId { get; init; }
    
    public Guid CvId { get; init; }

    private Cv()
    {
        
    }

    internal static Cv CreateCv(Guid applicantId, Guid cvId)
    {
        return new Cv
        {
            ApplicantId = applicantId,
            CvId = cvId,
        };
    }
}