using JPT.Core.Features.Users;
using JPT.UseCases.Features.Users.UpdateUser;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Users;

internal sealed class UpdateUser : IEndpoint
{
    private sealed record Request(
        string FirstName,
        string? MiddleName,
        string? LastName,
        UserRole Role,
        Guid? AvatarId,
        string CompanyName,
        string? CompanyDescription,
        Guid? LogoId);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users", async (
                Request request,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var command = new UpdateUserCommand(
                    request.FirstName,
                    request.MiddleName,
                    request.LastName,
                    request.Role,
                    request.AvatarId,
                    request.CompanyName,
                    request.CompanyDescription,
                    request.LogoId);

                var result = await mediator.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Users)
            .RequireAuthorization();
    }
}
