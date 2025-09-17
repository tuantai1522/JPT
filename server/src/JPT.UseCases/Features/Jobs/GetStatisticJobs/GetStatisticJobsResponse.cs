namespace JPT.UseCases.Features.Jobs.GetStatisticJobs;

public sealed record GetStatisticJobsResponse(int ActiveJobs, int TotalApplicants, int TotalHired);