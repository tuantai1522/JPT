using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.Users.LogOut;

public sealed record LogOutCommand : IRequest<Result<bool>>;
