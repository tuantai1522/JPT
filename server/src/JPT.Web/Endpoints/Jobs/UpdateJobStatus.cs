using JPT.Core.Features.Jobs;
using JPT.UseCases.Features.Jobs.UpdateJobStatus;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Jobs;

internal sealed class UpdateJobStatus : IEndpoint
{
    private sealed record Request(JobStatus Status);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("jobs/job-status/{id:guid}", async (
            Guid id,
            Request request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateJobStatusCommand(id, request.Status);

            var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Jobs)
        .RequireAuthorization();
    }
}
