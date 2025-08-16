namespace JPT.UseCases.Features.Jobs.GetJobsEmployer;

public sealed record GetJobsEmployerResponse(
    Guid Id,
    string Title,
    string? Description,
    string? Requirements,
    string CityName,
    string JobCategoryName,
    string Type,
    string CompanyName,
    string? CompanyDescription,
    decimal? MinSalary,
    decimal? MaxSalary,
    string Status,
    long CreatedAt,
    long? UpdatedAt,
    int CountApplications);