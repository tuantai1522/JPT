using JPT.UseCases.Features.Jobs.GetStatisticJobs;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Jobs;

internal sealed class GetStatisticJobs : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("jobs/get-statistic-jobs", async (
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new GetStatisticJobsQuery();

                var result = await mediator.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Jobs);
    }
}