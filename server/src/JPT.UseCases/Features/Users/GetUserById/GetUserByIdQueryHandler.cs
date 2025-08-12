using JPT.Core.Common;
using JPT.Core.Features.Files;
using JPT.Core.Features.Users;
using JPT.UseCases.Abstractions.Files;
using MediatR;

namespace JPT.UseCases.Features.Users.GetUserById;

internal sealed class GetUserByIdQueryHandler(
    IUserRepository userRepository, 
    IFileUrlResolver fileUrlResolver,
    IFileRepository fileRepository): IRequestHandler<GetUserByIdQuery, Result<GetUserByIdResponse>>
{
    public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(query.userId, cancellationToken, u => u.Cvs, u => u.Avatar, u => u.Company, u => u.Company!.Logo);

        if (user is null)
        {
            return Result.Failure<GetUserByIdResponse>(UserErrors.NotFoundByEmail);
        }

        var fileIds = user.Cvs.Select(cv => cv.CvId).ToList();

        var files = await fileRepository.GetFilesByIdAsync(fileIds, cancellationToken);
        
        var response = new GetUserByIdResponse(
            user.FirstName,
            user.MiddleName,
            user.LastName,
            user.Description,
            user.Avatar != null ? fileUrlResolver.Build(user.Avatar.UploadType, user.Avatar.Path) : null,
            user.Role.ToString(),
            user.Company?.Name,
            user.Company?.Description,
            user.Company?.Logo != null ? fileUrlResolver.Build(user.Company.Logo.UploadType, user.Company.Logo.Path) : null,
            files.Select(file => fileUrlResolver.Build(file.UploadType, file.Path)).ToList());
        
        return Result.Success(response);
    }
}
