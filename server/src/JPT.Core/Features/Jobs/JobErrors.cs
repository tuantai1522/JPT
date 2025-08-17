using JPT.Core.Common;

namespace JPT.Core.Features.Jobs;

public static class JobErrors
{
    public static Error NotFound(Guid jobId) => Error.NotFound(
        "Jobs.NotFound",
        $"The job with the Id = '{jobId}' was not found");
    
    public static Error AccessDenied(Guid jobId) => Error.Validation(
        "Jobs.AccessDenied",
        $"You don't have permission to access this resource.");
}
