using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.ApplyJob;

public sealed record ApplyJobCommand(Guid JobId, Guid CvId) : IRequest<Result<Guid>>;
