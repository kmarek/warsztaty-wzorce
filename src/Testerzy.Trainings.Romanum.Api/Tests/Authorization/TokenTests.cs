using FluentAssertions;
using NUnit.Framework.Internal;
using Testerzy.Trainings.Romanum.Framework.Api.Clients;
using Testerzy.Trainings.Romanum.Framework.Api.Exceptions;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Authorization;

public class TokenTests : BaseApiTest
{
    [Test]
    public void Verify_AccessToken_CanBeGeneratedFromCredentials()
    {
        Console.WriteLine($"Settings Api Url: {Settings.Api.Url}");
        
        var account = Settings.TestData.Accounts.First(u => u.Type == AccountType.Administrator);
        
        var tokens = OAuthTokenClient.GetTokenByPassword(account.Username, account.Password);
        tokens.Should().NotBeNull();
        tokens.AccessToken.Should().NotBeNullOrEmpty();
        tokens.RefreshToken.Should().NotBeNullOrEmpty();
    }

    [Test]
    public void Verify_AccessToken_CanBeGeneratedFromRefreshToken()
    {
        var account = Settings.TestData.Accounts.First(u => u.Type == AccountType.Administrator);
        var initialTokens = OAuthTokenClient.GetTokenByPassword(account.Username, account.Password);

        var refreshedTokens = OAuthTokenClient.GetTokenByRefreshToken(initialTokens.RefreshToken);

        refreshedTokens.Should().NotBeNull();
        refreshedTokens.AccessToken.Should().NotBeNullOrEmpty();
        refreshedTokens.RefreshToken.Should().NotBeNullOrEmpty();
        refreshedTokens.RefreshToken.Should().NotBe(initialTokens.RefreshToken, "the server rotates refresh tokens on every use");
    }

    [Test]
    public void Verify_ReusedRefreshToken_RevokesTokenFamily()
    {
        var account = Settings.TestData.Accounts.First(u => u.Type == AccountType.Administrator);
        var initialTokens = OAuthTokenClient.GetTokenByPassword(account.Username, account.Password);

        OAuthTokenClient.GetTokenByRefreshToken(initialTokens.RefreshToken);

        var act = () => OAuthTokenClient.GetTokenByRefreshToken(initialTokens.RefreshToken);

        act.Should().Throw<OAuthException>()
            .Where(e => e.Error.Error == "invalid_grant");
    }
}
