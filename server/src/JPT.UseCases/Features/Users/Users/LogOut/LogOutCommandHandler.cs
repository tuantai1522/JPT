using JPT.Core.Common;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Data;
using JPT.UseCases.Abstractions.WebStorages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JPT.UseCases.Features.Users.Users.LogOut;

internal sealed class LogOutCommandHandler(
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IApplicationDbContext dbContext,
    ICookieService cookieService): IRequestHandler<LogOutCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(LogOutCommand command, CancellationToken cancellationToken)
    {
        var refreshToken = cookieService.Get(Constant.RefreshTokenCookieName);
        
        cookieService.Delete(Constant.RefreshTokenCookieName);
        
        // Update empty token in database
        var token = await GetRefreshTokenByToken(refreshToken, cancellationToken);

        if (token is not null)
        {
            var user = await userRepository.GetUserByIdAsync(token.UserId, cancellationToken);

            user?.UpdateRefreshToken(token.Id, string.Empty, null);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return Result.Success(true);
    }
    
    private async Task<RefreshToken?> GetRefreshTokenByToken(string? token, CancellationToken cancellationToken)
    {
        var result = await dbContext.Set<RefreshToken>()
            .Where(c => c.Token == token)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }
}
