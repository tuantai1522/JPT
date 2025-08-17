using JPT.Core.Common;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Authentication;
using MediatR;

namespace JPT.UseCases.Features.Jobs.AddJob;

internal sealed class AddJobCommandHandler(
    IUnitOfWork unitOfWork,
    IUserProvider userProvider,
    IUserRepository userRepository) : IRequestHandler<AddJobCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddJobCommand command, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var user = await userRepository.GetUserByIdAsync(userId, cancellationToken, u => u.Company);
        
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(userId));
        }
        
        if (user.Role != UserRole.Employer)
        {
            return Result.Failure<Guid>(UserErrors.AccessDenied);
        }

        user.Company?.AddJob(command.Title,
            command.Description,
            command.Requirements,
            command.MinSalary,
            command.MaxSalary,
            command.JobCategoryId,
            command.Type,
            command.CityId);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return userId;
    }
}
