using Testerzy.Trainings.Romanum.Framework.Api.Responses;

namespace Testerzy.Trainings.Romanum.Framework.Api.Exceptions;

public sealed class OAuthException(OAuthErrorResponse error, int statusCode)
      : Exception($"OAuth error '{error.Error}' ({statusCode}): {error.ErrorDescription}")
{
    public OAuthErrorResponse Error { get; } = error;
    public int StatusCode { get; } = statusCode;
}
