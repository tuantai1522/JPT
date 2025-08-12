using JPT.Core.Common;

namespace JPT.Core.Features.Users;

public sealed record CvLimit
{
    public int Value { get; init; }

    private CvLimit(int value) => Value = value;
    
    public static Result<CvLimit> CreateCvLimit(int value)
    {
        if (value <= 0)
        {
            return Result.Failure<CvLimit>(UserErrors.InvalidNumberCv);
        }
        
        return new CvLimit(value);
    }
    
    // Default CV Limit is 3
    public static readonly CvLimit Default = new(3);
}