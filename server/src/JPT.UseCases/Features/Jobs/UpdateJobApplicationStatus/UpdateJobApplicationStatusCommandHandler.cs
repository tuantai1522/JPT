using JPT.Core.Common;
using JPT.Core.Features.Jobs;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using JPT.UseCases.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JPT.UseCases.Features.Jobs.UpdateJobApplicationStatus;

internal sealed class UpdateJobApplicationStatusCommandHandler(
    IUnitOfWork unitOfWork,
    IUserProvider userProvider,
    IApplicationDbContext dbContext) : IRequestHandler<UpdateJobApplicationStatusCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateJobApplicationStatusCommand command, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var job = await GetJobByIdWithApplicationsAndCompanyAsync(command.JobId, userId, cancellationToken);

        if (job is null)
        {
            return Result.Failure<Guid>(JobErrors.NotFound(command.JobId));
        }

        var result = job.UpdateJobApplicationStatus(command.JobApplicationId, command.Status);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return userId;
    }

    private async Task<Job?> GetJobByIdWithApplicationsAndCompanyAsync(Guid jobId, Guid employerId, CancellationToken ct)
    {
        return await 
        (
            from job in dbContext.Set<Job>().Include(j => j.JobApplications)
            join company in dbContext.Set<Company>() on job.CompanyId equals company.Id
            where job.Id == jobId && company.EmployerId == employerId
            select job
        ).FirstOrDefaultAsync(ct);
    }
}
