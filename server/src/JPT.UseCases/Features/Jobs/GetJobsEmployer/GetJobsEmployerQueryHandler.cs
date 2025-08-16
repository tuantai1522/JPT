using JPT.Core.Common;
using JPT.Core.Features.Countries;
using JPT.Core.Features.Jobs;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using JPT.UseCases.Abstractions.Data;
using JPT.UseCases.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JPT.UseCases.Features.Jobs.GetJobsEmployer;

public class GetJobsEmployerQueryHandler(
    IUserProvider userProvider,
    IUserRepository userRepository,
    IApplicationDbContext dbContext) : IRequestHandler<GetJobsEmployerQuery, Result<PaginationResponse<GetJobsEmployerResponse>>>
{
    public async Task<Result<PaginationResponse<GetJobsEmployerResponse>>> Handle(GetJobsEmployerQuery query, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var user = await userRepository.GetUserByIdAsync(userId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<PaginationResponse<GetJobsEmployerResponse>>(UserErrors.NotFound(userId));
        }

        if (user.Role != UserRole.Employer)
        {
            return Result.Failure<PaginationResponse<GetJobsEmployerResponse>>(UserErrors.AccessDenied);
        }

        var result = await BuildResponse(userId, query.Page, query.PageSize, cancellationToken);

        return Result.Success(result);
    }

    private async Task<PaginationResponse<GetJobsEmployerResponse>> BuildResponse(Guid userId, int page, int pageSize,
        CancellationToken cancellationToken)
    {
        // Only job entity needs AsNoTracking
        var query =
            from job in dbContext.Set<Job>().AsNoTracking()
            join company  in dbContext.Set<Company>()    on job.CompanyId     equals company.Id
            join category in dbContext.Set<JobCategory>()on job.JobCategoryId equals category.Id
            join city     in dbContext.Set<City>()       on job.CityId        equals city.Id
            join ja in dbContext.Set<JobApplication>()   on job.Id equals ja.JobId into jobApps
            where company.EmployerId == userId
            orderby job.UpdatedAt ?? job.CreatedAt descending
            select new GetJobsEmployerResponse(
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
                job.Status.ToString(),                  
                job.CreatedAt,
                job.UpdatedAt,
                jobApps.Count()              
            );

        return await PaginationResponse<GetJobsEmployerResponse>.CreateAsync(query, page, pageSize, cancellationToken);
    }
}