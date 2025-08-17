using JPT.UseCases.Features.Users.JobApplication.ApplyJob;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Users.JobApplication;

internal sealed class ApplyJob : IEndpoint
{
    private sealed record Request(Guid CvId);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/job-applications/{jobId:guid}", async (
            Guid jobId,
            Request request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var command = new ApplyJobCommand(jobId, request.CvId);

            var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users)
        .RequireAuthorization();
    }
}
