using JPT.Core.Common;
using JPT.Core.Features.Countries;
using JPT.Core.Features.Jobs;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using JPT.UseCases.Abstractions.Data;
using JPT.UseCases.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JPT.UseCases.Features.Users.SavedJobs.GetSavedJobs;

public class GetSavedJobsQueryHandler(
    IUserProvider userProvider,
    IApplicationDbContext dbContext) : IRequestHandler<GetSavedJobsQuery, Result<PaginationResponse<GetSavedJobsResponse>>>
{
    public async Task<Result<PaginationResponse<GetSavedJobsResponse>>> Handle(GetSavedJobsQuery query, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var result = await BuildResponse(userId, query, cancellationToken);

        return Result.Success(result);
    }

    private async Task<PaginationResponse<GetSavedJobsResponse>> BuildResponse(Guid userId, GetSavedJobsQuery request, CancellationToken cancellationToken)
    {
        // Only job entity needs AsNoTracking
        var query =
            from job in dbContext.Set<Job>().AsNoTracking()
            join company in dbContext.Set<Company>() on job.CompanyId equals company.Id
            join category in dbContext.Set<JobCategory>() on job.JobCategoryId equals category.Id
            join city in dbContext.Set<City>() on job.CityId equals city.Id
            join ja in dbContext.Set<JobApplication>() on job.Id equals ja.JobId into jobApps
            where dbContext.Set<SavedJob>().Any(sj => sj.JobId == job.Id && sj.ApplicantId == userId)
            orderby job.UpdatedAt ?? job.CreatedAt descending
            select new GetSavedJobsResponse(
                job.Id,
                job.Title,
                city.Name,
                category.Name,
                job.Type.ToString(),
                company.Name,
                job.MinSalary,
                job.MaxSalary,
                job.CreatedAt,
                jobApps
                    .Where(jobApp => jobApp.ApplicantId == userId)
                    .OrderByDescending(jobApp => jobApp.AppliedAt)
                    .Select(jobApp => jobApp.Status.ToString())
                    .FirstOrDefault()
            );

        return await PaginationResponse<GetSavedJobsResponse>.CreateAsync(query, request.Page, request.PageSize,
            cancellationToken);
    }
}