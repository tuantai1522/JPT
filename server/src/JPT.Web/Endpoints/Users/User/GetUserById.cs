using JPT.UseCases.Features.Users.User.GetUserById;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Users.User;

internal sealed class GetUserById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{id:guid}", async (
                Guid id,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var command = new GetUserByIdQuery(id);

                var result = await mediator.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Users);
    }
}
