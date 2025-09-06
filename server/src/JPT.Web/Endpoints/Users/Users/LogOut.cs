using JPT.UseCases.Features.Users.Users.LogOut;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Users.Users;

internal sealed class LogOut : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/logout", async (
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var command = new LogOutCommand();

            var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
