using JPT.Core.Common;
using JPT.Core.Features.Jobs;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using JPT.UseCases.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JPT.UseCases.Features.Jobs.UpdateJobStatus;

internal sealed class UpdateJobStatusCommandHandler(
    IUnitOfWork unitOfWork,
    IApplicationDbContext dbContext,
    IUserProvider userProvider,
    IUserRepository userRepository) : IRequestHandler<UpdateJobStatusCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateJobStatusCommand command, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var user = await userRepository.GetUserByIdAsync(userId, cancellationToken, u => u.Company);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(userId));
        }

        var job = await GetJobByIdAsync(command.Id, cancellationToken);

        if (job is null)
        {
            return Result.Failure<Guid>(JobErrors.NotFound(command.Id));
        }

        if (job.CompanyId != user.Company?.Id)
        {
            return Result.Failure<Guid>(JobErrors.AccessDenied(command.Id));
        }

        job.UpdateStatus(command.Status);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(job.Id);
    }

    private async Task<Job?> GetJobByIdAsync(Guid jobId, CancellationToken cancellationToken)
        => await dbContext.Set<Job>()
            .FirstOrDefaultAsync(job => job.Id == jobId, cancellationToken);
}
