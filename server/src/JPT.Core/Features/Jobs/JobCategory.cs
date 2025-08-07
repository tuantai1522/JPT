using JPT.Core.Common;

namespace JPT.Core.Features.Jobs;

public sealed class JobCategory : IAggregateRoot
{
    public int Id { get; init; }

    public string Name { get; private set; } = null!;
    
    /// <summary>
    /// List jobs of this category.
    /// </summary>
    private readonly List<Job> _jobs = [];
    
    public IReadOnlyList<Job> Jobs => _jobs.ToList();

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