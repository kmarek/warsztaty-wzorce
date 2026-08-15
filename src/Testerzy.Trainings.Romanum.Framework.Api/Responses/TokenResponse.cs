using System.Text.Json.Serialization;

namespace Testerzy.Trainings.Romanum.Framework.Api.Responses;

public sealed class TokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; } = default!;

    [JsonPropertyName("token_type")]
    public string TokenType { get; init; } = default!;

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; init; } = default!;

    [JsonPropertyName("scope")]
    public string? Scope { get; init; }
}
