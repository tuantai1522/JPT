using JPT.Core.Common;
using JPT.Core.Features.Countries;
using JPT.Core.Features.Jobs;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using JPT.UseCases.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JPT.UseCases.Features.Jobs.GetJobById;

public class GetJobByIdQueryHandler(
    IUserProvider userProvider,
    IApplicationDbContext dbContext) : IRequestHandler<GetJobByIdQuery, Result<GetJobByIdResponse>>
{
    public async Task<Result<GetJobByIdResponse>> Handle(GetJobByIdQuery query, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var response = await BuildResponse(query.JobId, userId, cancellationToken);

        if (response is null)
        {
            return Result.Failure<GetJobByIdResponse>(JobErrors.NotFound(query.JobId));
        }

        return Result.Success(response);
    }

    private async Task<GetJobByIdResponse?> BuildResponse(Guid jobId, Guid? userId, CancellationToken cancellationToken)
    {
        // Only job entity needs AsNoTracking
        var query =
            from job in dbContext.Set<Job>().AsNoTracking()
            join company  in dbContext.Set<Company>()     on job.CompanyId     equals company.Id
            join category in dbContext.Set<JobCategory>() on job.JobCategoryId equals category.Id
            join city     in dbContext.Set<City>()        on job.CityId        equals city.Id
            where job.Id == jobId
            orderby job.UpdatedAt ?? job.CreatedAt descending
            select new GetJobByIdResponse(
                job.Id,
                job.Title,
                job.Description,
                job.Requirements,
                city.Name,
                category.Name,
                job.Type.ToString(),
                company.Name,
                company.Description,
                job.MinSalary,
                job.MaxSalary,
                dbContext.Set<JobApplication>()
                    .Where(s => s.JobId == job.Id && s.ApplicantId == userId)
                    .OrderByDescending(s => s.AppliedAt)
                    .Select(s => s.Status.ToString())
                    .FirstOrDefault(),         
                job.CreatedAt,
                job.UpdatedAt
            );

        return await query.FirstOrDefaultAsync(cancellationToken);
    }
}