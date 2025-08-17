using JPT.Core.Common;
using JPT.UseCases.Features.Jobs.GetJobsEmployer;
using MediatR;

namespace JPT.UseCases.Features.Jobs.GetJobById;

public sealed record GetJobByIdQuery(Guid Id) : IRequest<Result<GetJobByIdResponse>>;