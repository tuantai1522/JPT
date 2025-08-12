using JPT.UseCases.Features.Files.AddCv;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Users;

internal sealed class AddCv : IEndpoint
{
    private sealed record Request(Guid FileId);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/add-cv", async (
            Request request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var command = new AddCvCommand(request.FileId);

            var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users)
        .RequireAuthorization();
    }
}
