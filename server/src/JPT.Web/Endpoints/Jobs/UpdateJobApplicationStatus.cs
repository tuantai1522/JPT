using JPT.Core.Features.Jobs;
using JPT.UseCases.Features.Jobs.UpdateJobApplicationStatus;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JPT.Web.Endpoints.Jobs;

internal sealed class UpdateJobApplicationStatus : IEndpoint
{
    private sealed record Request(JobApplicationStatus Status);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("jobs/{id:guid}/job-applications/{jobApplicationId:guid}", async (
                Guid id,
                Guid jobApplicationId,
                [FromBody] Request request,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var command = new UpdateJobApplicationStatusCommand(id, jobApplicationId, request.Status);

                var result = await mediator.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Jobs)
            .RequireAuthorization();
    }
}
