using System.Text.Json.Serialization;

namespace JPT.Core.Features.Jobs;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum JobApplicationStatus
{
    Apply = 0,
    InReview = 1,
    Rejected = 2,
    Accepted = 3
}