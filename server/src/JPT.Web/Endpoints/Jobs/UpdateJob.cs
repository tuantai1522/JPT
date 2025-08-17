using JPT.Core.Features.Jobs;
using JPT.UseCases.Features.Jobs.UpdateJob;
using JPT.Web.Extensions;
using JPT.Web.Infrastructure;
using MediatR;

namespace JPT.Web.Endpoints.Jobs;

internal sealed class UpdateJob : IEndpoint
{
    private sealed record Request(
        string Title,
        string? Description,
        string? Requirements,
        decimal? MinSalary,
        decimal? MaxSalary,
        int JobCategoryId,
        JobType Type,
        int CityId);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("jobs/{id:guid}", async (
            Guid id,
            Request request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateJobCommand(
                id,
                request.Title,
                request.Description,
                request.Requirements,
                request.MinSalary,
                request.MaxSalary,
                request.JobCategoryId,
                request.Type,
                request.CityId);

            var result = await mediator.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Jobs)
        .RequireAuthorization();
    }
}
