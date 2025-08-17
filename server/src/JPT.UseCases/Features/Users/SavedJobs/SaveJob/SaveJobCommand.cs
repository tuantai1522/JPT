using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.SavedJobs.SaveJob;

public sealed record SaveJobCommand(Guid JobId) : IRequest<Result<Guid>>;
