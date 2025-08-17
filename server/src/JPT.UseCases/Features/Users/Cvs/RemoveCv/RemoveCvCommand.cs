using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.Cvs.RemoveCv;

public sealed record RemoveCvCommand(Guid FileId) : IRequest<Result<Guid>>;
