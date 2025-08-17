using JPT.Core.Features.Jobs;
using JPT.UseCases.Pagination;

namespace JPT.UseCases.Features.Jobs.GetJobs;

public sealed record GetJobsQuery(
    string? KeyWords,
    int? CityId,
    IReadOnlyList<JobType> JobTypes,
    decimal? MinSalary,
    decimal? MaxSalary,
    IReadOnlyList<int> JobCategoryIds) : PaginationRequest<GetJobsResponse>;