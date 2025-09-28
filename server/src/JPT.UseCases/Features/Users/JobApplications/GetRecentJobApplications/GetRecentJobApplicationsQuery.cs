using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.JobApplications.GetRecentJobApplications;

public sealed record GetRecentJobApplicationsQuery : IRequest<Result<IReadOnlyList<GetRecentJobApplicationsResponse>>>;