namespace JPT.UseCases.Features.Users.SavedJobs.GetSavedJobs;

public sealed record GetSavedJobsResponse(
    Guid Id,
    string Title,
    string CityName,
    string JobCategoryName,
    string Type,
    string CompanyName,
    decimal? MinSalary,
    decimal? MaxSalary,
    long CreatedAt,
    string? JobApplicationStatus);