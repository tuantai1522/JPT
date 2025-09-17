using JPT.Core.Common;
using JPT.Core.Features.Jobs;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using JPT.UseCases.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JPT.UseCases.Features.Jobs.GetStatisticJobs;

public class GetStatisticJobsQueryHandler(
    IUserRepository userRepository,
    IUserProvider userProvider,
    IApplicationDbContext dbContext) : IRequestHandler<GetStatisticJobsQuery, Result<GetStatisticJobsResponse>>
{
    public async Task<Result<GetStatisticJobsResponse>> Handle(GetStatisticJobsQuery query, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var user = await userRepository.GetUserByIdAsync(userId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<GetStatisticJobsResponse>(UserErrors.NotFound(userId));
        }

        if (user.Role != UserRole.Employer)
        {
            return Result.Failure<GetStatisticJobsResponse>(UserErrors.AccessDenied);
        }
        
        var response = await BuildResponse(userId, cancellationToken);

        return Result.Success(response);
    }

    private async Task<GetStatisticJobsResponse> BuildResponse(Guid userId, CancellationToken cancellationToken)
    {
        // Count total active jobs
        var activeJobs = await (from job in dbContext.Set<Job>().AsNoTracking()
            join company in dbContext.Set<Company>() on job.CompanyId equals company.Id
            where company.EmployerId == userId &&
                  job.Status == JobStatus.Active
            select job).CountAsync(cancellationToken);
        
        // Total applicants
        var totalApplicants = await (from app in dbContext.Set<JobApplication>().AsNoTracking()
                    join job in dbContext.Set<Job>() on app.JobId equals job.Id
                    join company in dbContext.Set<Company>() on job.CompanyId equals company.Id
                    where company.EmployerId == userId
                    group app by app.ApplicantId into g
                    select g.Key
                ).CountAsync(cancellationToken);
        
        // Total hired
        var totalHired = await (from app in dbContext.Set<JobApplication>().AsNoTracking()
                join job in dbContext.Set<Job>() on app.JobId equals job.Id
                join company in dbContext.Set<Company>() on job.CompanyId equals company.Id
                where company.EmployerId == userId &&
                      app.Status == JobApplicationStatus.Accepted
                group app by app.ApplicantId into g
                select g.Key
            ).CountAsync(cancellationToken);

        return new GetStatisticJobsResponse(activeJobs, totalApplicants, totalHired);
    }
}