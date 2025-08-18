using JPT.Core.Common;
using JPT.Core.Features.Jobs;
using JPT.UseCases.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JPT.UseCases.Features.Jobs.GetJobCategories;

public class GetJobCategoriesQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetJobCategoriesQuery, Result<IReadOnlyList<GetJobCategoriesResponse>>>
{
    public async Task<Result<IReadOnlyList<GetJobCategoriesResponse>>> Handle(GetJobCategoriesQuery query, CancellationToken cancellationToken)
    {
        var response = await BuildResponse(cancellationToken);

        return Result.Success(response);
    }

    private async Task<IReadOnlyList<GetJobCategoriesResponse>> BuildResponse(CancellationToken cancellationToken)
    {
        return await dbContext.Set<JobCategory>()
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .Select(x => new GetJobCategoriesResponse(x.Id, x.Name))
            .ToListAsync(cancellationToken);
    }
}