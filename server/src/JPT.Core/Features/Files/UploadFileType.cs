using System.Text.Json.Serialization;

namespace JPT.Core.Features.Files;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UploadFileType
{
    LocalHost = 0,
    Firebase = 1
}