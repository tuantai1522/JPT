using JPT.Core.Common;
using JPT.Core.Features.Jobs;
using MediatR;

namespace JPT.UseCases.Features.Jobs.UpdateJobApplicationStatus;

public sealed record UpdateJobApplicationStatusCommand(Guid JobId, Guid JobApplicationId, JobApplicationStatus Status) : IRequest<Result<Guid>>;
