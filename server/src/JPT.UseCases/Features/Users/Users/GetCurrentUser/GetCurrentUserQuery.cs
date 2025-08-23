using JPT.Core.Common;
using JPT.UseCases.Features.Users.Users.GetUserById;
using MediatR;

namespace JPT.UseCases.Features.Users.Users.GetCurrentUser;

public sealed record GetCurrentUserQuery : IRequest<Result<GetCurrentUserResponse>>;
