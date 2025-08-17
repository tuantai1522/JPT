using JPT.UseCases.Features.Users.SavedJob.SaveJob;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Users.SavedJob;

internal sealed class SaveJob : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/saved-jobs/{jobId:guid}", async (
            Guid jobId,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var command = new SaveJobCommand(jobId);

            var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users)
        .RequireAuthorization();
    }
}
