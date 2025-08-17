namespace JPT.UseCases.Features.Jobs.GetJobs;

public sealed record GetJobsResponse(
    Guid Id,
    string Title,
    string CityName,
    string JobCategoryName,
    string Type,
    string CompanyName,
    decimal? MinSalary,
    decimal? MaxSalary,
    long CreatedAt,
    string JobApplicationStatus,
    bool IsSaved);