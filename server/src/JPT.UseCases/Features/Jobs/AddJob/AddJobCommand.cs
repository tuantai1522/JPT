using JPT.Core.Common;
using JPT.Core.Features.Jobs;
using MediatR;

namespace JPT.UseCases.Features.Jobs.AddJob;

public sealed record AddJobCommand(
    string Title, 
    string? Description, 
    string? Requirements, 
    decimal? MinSalary, 
    decimal? MaxSalary, 
    int JobCategoryId, 
    JobType Type, 
    int CityId) : IRequest<Result<Guid>>;
