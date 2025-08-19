using JPT.UseCases.Features.Jobs.GetJobCategories;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.JobCategories;

internal sealed class GetJobCategories : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("job-categories", async (
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new GetJobCategoriesQuery();

                var result = await mediator.Send(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.JobCategories);
    }
}
