using JPT.UseCases.Pagination;

namespace JPT.UseCases.Features.Users.JobApplications.GetRecentJobApplications;

public sealed record GetRecentJobApplicationsQuery : PaginationRequest<GetRecentJobApplicationsResponse>;