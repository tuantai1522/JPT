using JPT.UseCases.Features.Users.JobApplications.GetApplicantsByJobId;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Users.JobApplications;

internal sealed class GetApplicantsByJobId : IEndpoint
{
    private sealed record Request(int Page, int PageSize);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("jobs/job-applicants/{id:guid}", async (
                Guid id,
                [AsParameters] Request request,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new GetApplicantsByJobIdQuery(id)
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
