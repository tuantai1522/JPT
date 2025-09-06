using JPT.UseCases.Features.Users.Users.RenewAccessToken;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Users.Users;

internal sealed class RenewAccessToken : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/renew-access-token", async (
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new RenewAccessTokenQuery();

                var result = await mediator.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Users);
    }
}
