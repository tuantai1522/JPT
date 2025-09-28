namespace JPT.UseCases.Features.Users.JobApplications.GetRecentJobApplications;

public sealed record GetRecentJobApplicationsResponse(
    Guid Id,
    string FirstName,
    string? MiddleName,
    string? LastName,
    string? AvatarUrl,
    string Title,
    long AppliedAt);