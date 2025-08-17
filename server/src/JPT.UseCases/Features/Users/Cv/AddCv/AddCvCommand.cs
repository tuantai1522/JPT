using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.Cv.AddCv;

public sealed record AddCvCommand(Guid FileId) : IRequest<Result<Guid>>;
