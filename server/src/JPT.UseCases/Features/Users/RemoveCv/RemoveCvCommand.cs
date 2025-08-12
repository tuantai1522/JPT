using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.RemoveCv;

public sealed record RemoveCvCommand(Guid FileId) : IRequest<Result<Guid>>;
