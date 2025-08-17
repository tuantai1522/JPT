using JPT.Core.Common;
using JPT.Core.Features.Users;
using MediatR;

namespace JPT.UseCases.Features.Users.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string FirstName,
    string? MiddleName,
    string? LastName,
    string Email, 
    string Password,
    Guid? AvatarId, 
    UserRole Role,
    string CompanyName,
    Guid? LogoId) : IRequest<Result<Guid>>;
