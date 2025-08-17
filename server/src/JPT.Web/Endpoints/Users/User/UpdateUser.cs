using JPT.UseCases.Features.Users.User.UpdateUser;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Users.User;

internal sealed class UpdateUser : IEndpoint
{
    private sealed record Request(
        string FirstName,
        string? MiddleName,
        string? LastName,
        Guid? AvatarId,
        string? Description,
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
                    request.AvatarId,
                    request.Description,
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
