using JPT.Core.Features.Users;
using JPT.UseCases.Features.Users.RegisterUser;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Users;

internal sealed class RegisterUser : IEndpoint
{
    private sealed record Request(
        string FirstName,
        string? MiddleName,
        string? LastName,
        string Email,
        string Password,
        Guid? AvatarId,
        UserRole Role,
        string CompanyName,
        Guid? LogoId);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users", async (
            Request request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var command = new RegisterUserCommand(request.FirstName, 
                request.MiddleName, 
                request.LastName, 
                request.Email, 
                request.Password, 
                request.AvatarId, 
                request.Role, 
                request.CompanyName, 
                request.LogoId);

            var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
