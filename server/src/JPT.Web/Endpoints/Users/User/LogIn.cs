using JPT.UseCases.Features.Users.User.LogIn;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Users.User;

internal sealed class LogIn : IEndpoint
{
    private sealed record Request(
        string Email,
        string Password);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/login", async (
            Request request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var command = new LogInCommand(request.Email, request.Password);

            var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
