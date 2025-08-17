using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.SavedJob.SaveJob;

public sealed record SaveJobCommand(Guid JobId) : IRequest<Result<Guid>>;
