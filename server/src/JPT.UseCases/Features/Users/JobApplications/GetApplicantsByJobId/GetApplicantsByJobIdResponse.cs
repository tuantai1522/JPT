using System.Text.Json.Serialization;
using JPT.Core.Features.Files;

namespace JPT.UseCases.Features.Users.JobApplications.GetApplicantsByJobId;

public sealed record GetApplicantsByJobIdResponse(
    Guid Id,
    string FirstName,
    string? MiddleName,
    string? LastName,
    string Email,
    [property: JsonIgnore] UploadFileType UploadAvatarType,
    string? AvatarUrl,
    [property: JsonIgnore] UploadFileType UploadCvType,
    string CvUrl,
    Guid JobApplicationId,
    string JobApplicationStatus,
    long AppliedAt);