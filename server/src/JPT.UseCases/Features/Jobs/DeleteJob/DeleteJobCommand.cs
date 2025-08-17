using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Jobs.DeleteJob;

public sealed record DeleteJobCommand(Guid Id) : IRequest<Result<Guid>>;
