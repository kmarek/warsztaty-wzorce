using System.Text.Json.Serialization;

namespace Testerzy.Trainings.Romanum.Framework.Api.Responses;

public sealed class OAuthErrorResponse
{
    [JsonPropertyName("error")]
    public string Error { get; init; } = default!;

    [JsonPropertyName("error_description")]
    public string? ErrorDescription { get; init; }
}
