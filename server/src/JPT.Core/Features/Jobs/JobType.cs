using System.Text.Json.Serialization;

namespace JPT.Core.Features.Jobs;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum JobType
{
    Internship = 0,
    FullTime = 1,
    PartTime = 2,
    Remote = 3,
    Contract = 4,
}