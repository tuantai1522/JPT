using FluentValidation;
using JPT.Core.Features.Countries;
using JPT.Core.Features.Jobs;
using JPT.UseCases.Abstractions.Data;
using Microsoft.EntityFrameworkCore;

namespace JPT.UseCases.Features.Jobs.AddJob;

internal sealed class AddJobCommandValidator: AbstractValidator<AddJobCommand>
{
    private readonly IApplicationDbContext _dbContext;
    
    public AddJobCommandValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        
        RuleFor(c => c.Title)
            .Must(title => !string.IsNullOrWhiteSpace(title)).WithMessage("Title is required.");
        
        RuleFor(x => x.JobCategoryId)
            .GreaterThan(0).WithMessage("JobCategoryId is required.")
            .MustAsync(JobCategoryExists).WithMessage("Job category not found.");

        RuleFor(x => x.CityId)
            .GreaterThan(0).WithMessage("CityId is required.")
            .MustAsync(CityExists).WithMessage("City not found.");
        
        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid Job Type.");
    }
    
    private Task<bool> CityExists(int id, CancellationToken ct) =>
        _dbContext.Set<City>().AsNoTracking().AnyAsync(c => c.Id == id, ct);

    private Task<bool> JobCategoryExists(int id, CancellationToken ct) =>
        _dbContext.Set<JobCategory>().AsNoTracking().AnyAsync(c => c.Id == id, ct);
}
