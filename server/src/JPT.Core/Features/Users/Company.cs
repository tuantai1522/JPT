using JPT.Core.Common;
using JPT.Core.Features.Jobs;

namespace JPT.Core.Features.Users;

public sealed class Company : IBaseEntity
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    
    public string Name { get; private set; } = null!;
    
    public string? Description { get; private set; }
    
    public Guid EmployerId { get; private set; }
    
    public Guid? LogoId { get; private set; }

    /// <summary>
    /// List jobs of this company.
    /// </summary>
    private readonly List<Job> _jobs = [];
    
    public IReadOnlyList<Job> Jobs => _jobs.ToList();
    internal static Company CreateCompany(string name, Guid employerId, Guid? logoId)
    {
        return new Company
        {
            Name = name,
            EmployerId = employerId,
            LogoId = logoId,
        };
    }

    public void UpdateCompany(string name, string? description, Guid? logoId)
    {
        Name = name;
        Description = description;
        LogoId = logoId;
    }

    public void AddJob(string title, string? description, string? requirements, decimal? minSalary,
        decimal? maxSalary, int jobCategoryId, JobType type, int cityId)
    {
        var newJob = Job.CreateJob(title, description, requirements, minSalary, maxSalary, jobCategoryId, Id,
            type, cityId);
        
        _jobs.Add(newJob);
    }

}