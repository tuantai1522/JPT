using JPT.Core.Features.Jobs;
using JPT.UseCases.Pagination;

namespace JPT.UseCases.Features.Jobs.GetJobs;

public sealed record GetJobsQuery(
    string? KeyWords,
    int? CityId,
    List<JobType>? JobTypes,
    decimal? MinSalary,
    decimal? MaxSalary,
    List<int>? JobCategoryIds) : PaginationRequest<GetJobsResponse>;