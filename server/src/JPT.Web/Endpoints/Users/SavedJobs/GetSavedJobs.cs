using JPT.UseCases.Features.Users.SavedJobs.GetSavedJobs;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Users.SavedJobs;

internal sealed class GetSavedJobs : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/saved-jobs", async (
                [AsParameters] GetSavedJobsQuery query,
                IMediator mediator,
                CancellationToken ct) =>
            {
                var result = await mediator.Send(query, ct);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Users);
    }
}
