using JPT.Core.Common;

namespace JPT.Core.Features.Jobs;

public sealed class JobCategory : IAggregateRoot
{
    public int Id { get; init; }

    public string Name { get; private set; } = null!;

    private JobCategory()
    {
        
    }
    
    public static JobCategory CreateJobCategory(string name)
    {
        return new JobCategory
        {
            Name = name,
        };
    }
}