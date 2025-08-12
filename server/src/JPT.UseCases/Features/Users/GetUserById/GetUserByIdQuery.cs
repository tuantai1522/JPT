using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.GetUserById;

public sealed record GetUserByIdQuery(Guid userId) : IRequest<Result<GetUserByIdResponse>>;
