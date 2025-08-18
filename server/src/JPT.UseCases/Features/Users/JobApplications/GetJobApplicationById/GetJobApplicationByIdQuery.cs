using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.JobApplications.GetJobApplicationById;

public sealed record GetJobApplicationByIdQuery(Guid JobApplicationId) : IRequest<Result<GetJobApplicationByIdResponse>>;