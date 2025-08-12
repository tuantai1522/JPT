using JPT.UseCases.Features.Users.AddCv;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Users;

internal sealed class AddCv : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/cvs/{fileId:guid}", async (
            Guid fileId,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var command = new AddCvCommand(fileId);

            var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users)
        .RequireAuthorization();
    }
}
