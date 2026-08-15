using RestSharp;
using Testerzy.Trainings.Romanum.Framework.Api.Clients;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api;

public class BaseApiTest
{
    protected static Settings Settings => GlobalSetup.Settings;
    protected RestClient RestClient { get; private set; }
    protected OAuthTokenClient OAuthTokenClient { get; set; }

    [OneTimeSetUp]
    public void BaseOneTimeSetup()
    {
        RestClient = new RestClient(Settings.Api.Url); 
        OAuthTokenClient = new OAuthTokenClient(Settings.Api.Url, Settings.Api.ClientId, Settings.Api.ClientSecret, Settings.Api.BypassKey);
    }

    [OneTimeTearDown]
    public void BaseOneTimeTearDown() 
    {
        RestClient.Dispose();
    }

    protected void AddAuthHeaders(RestRequest request, string accessToken)
    {
        if (Settings.Api.BypassKey is not null)
        {
            request.AddHeader("x-vercel-protection-bypass", Settings.Api.BypassKey);
            request.AddHeader("x-vercel-set-bypass-cookie", true);
        }
        request.AddHeader("Authorization", $"Bearer {accessToken}");
    }
}
