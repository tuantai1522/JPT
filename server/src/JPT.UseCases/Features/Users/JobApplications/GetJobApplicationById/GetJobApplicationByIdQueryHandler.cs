using JPT.Core.Common;
using JPT.Core.Features.Countries;
using JPT.Core.Features.Jobs;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using JPT.UseCases.Abstractions.Data;
using JPT.UseCases.Abstractions.Files;
using MediatR;
using Microsoft.EntityFrameworkCore;
using File = JPT.Core.Features.Files.File;

namespace JPT.UseCases.Features.Users.JobApplications.GetJobApplicationById;

public class GetJobApplicationByIdQueryHandler(
    IFileUrlResolver fileUrlResolver,
    IUserProvider userProvider,
    IApplicationDbContext dbContext) : IRequestHandler<GetJobApplicationByIdQuery, Result<GetJobApplicationByIdResponse>>
{
    public async Task<Result<GetJobApplicationByIdResponse>> Handle(GetJobApplicationByIdQuery query, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var result = await BuildResponse(query.JobApplicationId, userId, cancellationToken);

        if (result is null)
        {
            return Result.Failure<GetJobApplicationByIdResponse>(JobErrors.JobApplicationNotFound(query.JobApplicationId));
        }
        
        if (!string.IsNullOrEmpty(result.AvatarUrl))
        {
            result = result with { AvatarUrl = fileUrlResolver.Build(result.UploadAvatarType, result.AvatarUrl) };
        }

        if (!string.IsNullOrEmpty(result.CvUrl))
        {
            result = result with { CvUrl = fileUrlResolver.Build(result.UploadCvType, result.CvUrl) };
        }

        return result;
    }

    private async Task<GetJobApplicationByIdResponse?> BuildResponse(Guid jobApplicationId, Guid userId, CancellationToken cancellationToken)
    {
        // Only job entity needs AsNoTracking
        var query =
            from ja in dbContext.Set<JobApplication>().AsNoTracking()
            where ja.Id == jobApplicationId
            join user in dbContext.Set<User>() on ja.ApplicantId equals user.Id
            join job  in dbContext.Set<Job>()  on ja.JobId      equals job.Id
            join city in dbContext.Set<City>() on job.CityId    equals city.Id
            join company in dbContext.Set<Company>() on job.CompanyId equals company.Id

            from avFile in dbContext.Set<File>()
                .Where(f => user.AvatarId != null && f.Id == user.AvatarId)
                .DefaultIfEmpty()

            join cvFile in dbContext.Set<File>() on ja.CvId equals cvFile.Id
            
            // Only applicant or Employer can view this job application
            where userId == ja.ApplicantId || userId == company.EmployerId
            
            select new GetJobApplicationByIdResponse(
                user.Id,
                user.FirstName,
                user.MiddleName,
                user.LastName,
                user.Email,
                avFile.UploadType,
                avFile.Path,
                
                job.Id,
                job.Title,
                city.Name,
                job.Type.ToString(),
                
                cvFile.UploadType,
                cvFile.Path,
                ja.Status.ToString(),
                ja.AppliedAt
            );

        var result = await query.FirstOrDefaultAsync(cancellationToken);
        return result;
    }
}