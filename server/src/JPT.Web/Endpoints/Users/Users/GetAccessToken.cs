using JPT.UseCases.Features.Users.Users.GetAccessToken;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Users.Users;

internal sealed class GetAccessToken : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/get-access-token", async (
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new GetAccessTokenQuery();

                var result = await mediator.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Users);
    }
}
