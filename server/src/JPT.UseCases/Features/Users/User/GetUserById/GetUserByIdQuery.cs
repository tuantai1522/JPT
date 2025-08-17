using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.User.GetUserById;

public sealed record GetUserByIdQuery(Guid UserId) : IRequest<Result<GetUserByIdResponse>>;
