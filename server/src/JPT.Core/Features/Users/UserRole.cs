using System.Text.Json.Serialization;

namespace JPT.Core.Features.Users;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRole
{
    Employer = 0,
    JobSeeker = 1
}