using JPT.UseCases.Features.Users.JobApplications.GetRecentJobApplications;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Users.JobApplications;

internal sealed class GetRecentJobApplications : IEndpoint
{
    private sealed record Request(int Page, int PageSize);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("jobs/get-recent-job-applications", async (
                [AsParameters] Request request,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new GetRecentJobApplicationsQuery()
                {
                    Page = request.Page,
                    PageSize = request.PageSize
                };

                var result = await mediator.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Jobs)
            .RequireAuthorization();
    }
}