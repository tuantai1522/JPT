using System.Text.Json.Serialization;

namespace JPT.Core.Features.Jobs;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum JobStatus
{
    Active = 0,
    Closed = 1
}