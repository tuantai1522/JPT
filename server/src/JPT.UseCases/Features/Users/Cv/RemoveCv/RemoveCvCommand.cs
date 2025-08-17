using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.Cv.RemoveCv;

public sealed record RemoveCvCommand(Guid FileId) : IRequest<Result<Guid>>;
