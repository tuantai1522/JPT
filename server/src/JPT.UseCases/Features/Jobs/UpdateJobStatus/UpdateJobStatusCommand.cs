using JPT.Core.Common;
using JPT.Core.Features.Jobs;
using MediatR;

namespace JPT.UseCases.Features.Jobs.UpdateJobStatus;

public sealed record UpdateJobStatusCommand(Guid Id, JobStatus Status) : IRequest<Result<Guid>>;
