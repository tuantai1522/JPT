using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.SavedJob.UnSaveJob;

public sealed record UnSaveJobCommand(Guid JobId) : IRequest<Result<Guid>>;
