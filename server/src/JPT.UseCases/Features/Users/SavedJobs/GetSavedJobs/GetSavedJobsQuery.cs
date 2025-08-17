using JPT.UseCases.Pagination;

namespace JPT.UseCases.Features.Users.SavedJobs.GetSavedJobs;

public sealed record GetSavedJobsQuery : PaginationRequest<GetSavedJobsResponse>;