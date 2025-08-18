using JPT.UseCases.Pagination;

namespace JPT.UseCases.Features.Users.JobApplications.GetApplicantsByJobId;

public sealed record GetApplicantsByJobIdQuery(Guid Id) : PaginationRequest<GetApplicantsByJobIdResponse>;