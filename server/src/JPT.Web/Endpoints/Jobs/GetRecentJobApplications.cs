using JPT.UseCases.Features.Users.JobApplications.GetRecentJobApplications;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Jobs;

internal sealed class GetRecentJobApplications : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("jobs/get-recent-job-applications", async (
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new GetRecentJobApplicationsQuery();

                var result = await mediator.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Jobs)
            .RequireAuthorization();
    }
}