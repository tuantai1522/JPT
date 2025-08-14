using JPT.Core.Features.Jobs;
using File = JPT.Core.Features.Files.File;

namespace JPT.Core.Features.Users;

public sealed class Company
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    
    public string Name { get; private set; } = null!;
    
    public string? Description { get; private set; }
    
    public Guid EmployerId { get; private set; }
    public User Employer { get; private set; } = null!;
    
    public Guid? LogoId { get; private set; }
    public File? Logo { get; private set; }

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

}