using JPT.Core.Common;
using MediatR;

namespace JPT.UseCases.Features.Users.LogIn;

public sealed record LogInCommand(string Email, string Password) : IRequest<Result<LogInResponse>>;
