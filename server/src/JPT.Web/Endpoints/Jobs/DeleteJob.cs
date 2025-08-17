using JPT.UseCases.Features.Jobs.DeleteJob;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Jobs;

internal sealed class DeleteJob : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("jobs/{id:guid}", async (
                Guid id,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var command = new DeleteJobCommand(id);


                var result = await mediator.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Jobs)
            .RequireAuthorization();
    }
}
