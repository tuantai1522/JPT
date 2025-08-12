using JPT.UseCases.Features.Users.RemoveCv;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Users;

internal sealed class RemoveCv : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("users/cvs/{fileId:guid}", async (
            Guid fileId,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var command = new RemoveCvCommand(fileId);

            var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users)
        .RequireAuthorization();
    }
}
