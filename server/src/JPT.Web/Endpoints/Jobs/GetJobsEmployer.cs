using JPT.UseCases.Features.Jobs.GetJobsEmployer;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Jobs;

internal sealed class GetJobsEmployer : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("jobs/employer", async (
                [AsParameters] GetJobsEmployerQuery query,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Jobs);
    }
}
