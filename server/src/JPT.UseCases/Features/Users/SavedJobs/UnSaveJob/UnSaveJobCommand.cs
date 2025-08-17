using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.SavedJobs.UnSaveJob;

public sealed record UnSaveJobCommand(Guid JobId) : IRequest<Result<Guid>>;
