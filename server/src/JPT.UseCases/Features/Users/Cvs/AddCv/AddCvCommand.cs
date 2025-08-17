using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.Cvs.AddCv;

public sealed record AddCvCommand(Guid FileId) : IRequest<Result<Guid>>;
