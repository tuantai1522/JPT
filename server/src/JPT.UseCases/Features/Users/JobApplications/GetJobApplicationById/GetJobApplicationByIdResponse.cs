using System.Text.Json.Serialization;
using JPT.Core.Features.Files;

namespace JPT.UseCases.Features.Users.JobApplications.GetJobApplicationById;

public sealed record GetJobApplicationByIdResponse(
    Guid Id,
    string FirstName,
    string? MiddleName,
    string? LastName,
    string Email,
    [property: JsonIgnore] UploadFileType UploadAvatarType,
    string? AvatarUrl,
    
    Guid JobId,
    string Title,
    string CityName,
    string Type,
    
    [property: JsonIgnore] UploadFileType UploadCvType,
    string CvUrl,
    string JobApplicationStatus,
    long AppliedAt);