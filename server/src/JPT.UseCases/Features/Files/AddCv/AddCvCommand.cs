using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Files.AddCv;

public sealed record AddCvCommand(Guid FileId) : IRequest<Result<Guid>>;
