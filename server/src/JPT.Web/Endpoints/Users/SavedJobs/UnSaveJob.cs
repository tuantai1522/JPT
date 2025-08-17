using JPT.UseCases.Features.Users.SavedJobs.UnSaveJob;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Users.SavedJobs;

internal sealed class UnSaveJob : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users/saved-jobs/unsave-job/{jobId:guid}", async (
            Guid jobId,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var command = new UnSaveJobCommand(jobId);

            var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users)
        .RequireAuthorization();
    }
}
