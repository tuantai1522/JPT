using JPT.Core.Common;

namespace JPT.Core.Features.Jobs;

public static class JobErrors
{
    public static Error NotFound(Guid jobId) => Error.NotFound(
        "Users.NotFound",
        $"The job with the Id = '{jobId}' was not found");
}
