using JPT.Core.Common;
using JPT.Core.Features.Countries;
using JPT.Core.Features.Jobs;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using JPT.UseCases.Abstractions.Data;
using JPT.UseCases.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JPT.UseCases.Features.Jobs.GetJobs;

public class GetJobsQueryHandler(
    IUserProvider userProvider,
    IApplicationDbContext dbContext) : IRequestHandler<GetJobsQuery, Result<PaginationResponse<GetJobsResponse>>>
{
    public async Task<Result<PaginationResponse<GetJobsResponse>>> Handle(GetJobsQuery query, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var result = await BuildResponse(userId, query, cancellationToken);

        return Result.Success(result);
    }

    private async Task<PaginationResponse<GetJobsResponse>> BuildResponse(Guid? userId, GetJobsQuery request, CancellationToken cancellationToken)
    {
        // Only job entity needs AsNoTracking
        var query =
            from job in dbContext.Set<Job>().AsNoTracking()
            join company in dbContext.Set<Company>() on job.CompanyId equals company.Id
            join category in dbContext.Set<JobCategory>() on job.JobCategoryId equals category.Id
            join city in dbContext.Set<City>() on job.CityId equals city.Id
            join ja in dbContext.Set<JobApplication>() on job.Id equals ja.JobId into jobApps
            where
                // Todo: need to fix search with key word
                // string.IsNullOrEmpty(request.KeyWords) || EF.Functions.Like(job.Title, $"%{request.KeyWords}%") &&
                (!request.CityId.HasValue || job.CityId == request.CityId.Value) &&
                (request.JobTypes == null || request.JobTypes.Count == 0 || request.JobTypes.Contains(job.Type)) &&
                (request.JobCategoryIds == null || request.JobCategoryIds.Count == 0 || request.JobCategoryIds.Contains(job.JobCategoryId)) &&
                (request.MinSalary == null || (job.MaxSalary ?? decimal.MaxValue) >= request.MinSalary) &&
                (request.MaxSalary == null || (job.MinSalary ?? decimal.MinValue) <= request.MaxSalary)
            orderby job.UpdatedAt ?? job.CreatedAt descending
            select new GetJobsResponse(
                job.Id,
                job.Title,
                city.Name,
                category.Name,
                job.Type.ToString(),
                company.Name,
                job.MinSalary,
                job.MaxSalary,
                job.CreatedAt,
                userId.HasValue ? jobApps
                    .OrderByDescending(jobApp => jobApp.AppliedAt)
                    .FirstOrDefault(jobApp => jobApp.ApplicantId == userId).Status
                    .ToString() 
                    : JobApplicationStatus.Applied.ToString(),
                userId.HasValue && dbContext.Set<SavedJob>().Any(savedJob => savedJob.JobId == job.Id && savedJob.ApplicantId == userId)
            );

        return await PaginationResponse<GetJobsResponse>.CreateAsync(query, request.Page, request.PageSize,
            cancellationToken);
    }
}