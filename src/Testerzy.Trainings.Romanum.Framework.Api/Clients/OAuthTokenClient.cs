using RestSharp;
using Testerzy.Trainings.Romanum.Framework.Api.Exceptions;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;

namespace Testerzy.Trainings.Romanum.Framework.Api.Clients;

public sealed class OAuthTokenClient
{
    private readonly RestClient _client;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string? _bypassKey;

    public OAuthTokenClient(string baseUrl, string clientId, string clientSecret, string? bypassKey = null)
    {
        _client = new RestClient(baseUrl);
        _clientId = clientId;
        _clientSecret = clientSecret;
        _bypassKey = bypassKey;
    }

    public TokenResponse GetTokenByPassword(
        string username,
        string password)
    {
        var request = new RestRequest("/api/oauth/token", Method.Post)
            .AddParameter("grant_type", "password")
            .AddParameter("client_id", _clientId)
            .AddParameter("client_secret", _clientSecret)
            .AddParameter("username", username)
            .AddParameter("password", password);

        if (!string.IsNullOrEmpty(_bypassKey))
        {
            Console.WriteLine($"Using bypass key.");
            request.AddHeader("x-vercel-protection-bypass", _bypassKey);
            request.AddHeader("x-vercel-set-bypass-cookie", true);
        }

        return Execute(request);
    }

    public TokenResponse GetTokenByRefreshToken(string refreshToken)
    {
        Console.WriteLine($"Generating access token from refresh token {refreshToken}");

        var request = new RestRequest("/api/oauth/token", Method.Post)
            .AddParameter("grant_type", "refresh_token")
            .AddParameter("client_id", _clientId)
            .AddParameter("client_secret", _clientSecret)
            .AddParameter("refresh_token", refreshToken);

        return Execute(request);
    }

    private TokenResponse Execute(RestRequest request)
    {
        var response = _client.Execute<TokenResponse>(request);

        Console.WriteLine(response.StatusCode);
        Console.WriteLine(response.Content);

        if (!response.IsSuccessful || response.Data is null)
        {
            var error = response.Content is { Length: > 0 }
                ? System.Text.Json.JsonSerializer.Deserialize<OAuthErrorResponse>(response.Content)
                : null;

            throw new OAuthException(
                error ?? new OAuthErrorResponse { Error = "unknown_error", ErrorDescription = response.ErrorMessage },
                (int)response.StatusCode);
        }

        return response.Data;
    }
}
