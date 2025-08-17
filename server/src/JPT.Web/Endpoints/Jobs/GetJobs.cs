using JPT.UseCases.Features.Jobs.GetJobs;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JPT.Web.Endpoints.Jobs;

internal sealed class GetJobs : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("jobs/get-jobs", async (
                [FromBody] GetJobsQuery query,
                IMediator mediator,
                CancellationToken ct) =>
            {
                var result = await mediator.Send(query, ct);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Jobs);
    }
}
