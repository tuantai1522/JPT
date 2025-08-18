using JPT.UseCases.Features.Users.JobApplications.GetJobApplicationById;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Users.JobApplications;

internal sealed class GetJobApplicationById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/job-applications/{jobApplicationId:guid}", async (
                Guid jobApplicationId,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new GetJobApplicationByIdQuery(jobApplicationId);

                var result = await mediator.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Users)
            .RequireAuthorization();
    }
}
