using JPT.UseCases.Features.Users.SavedJobs.SaveJob;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Users.SavedJobs;

internal sealed class SaveJob : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/saved-jobs/save-job/{jobId:guid}", async (
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
