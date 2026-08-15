using RestSharp;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Seasons;

public abstract class SeasonTestsBase : BaseApiTest
{
    private readonly List<string> _seasonIdsToCleanUp = [];

    [TearDown]
    public void SeasonTestsTearDown()
    {
        if (_seasonIdsToCleanUp.Count == 0)
        {
            return;
        }

        foreach (var seasonId in _seasonIdsToCleanUp)
        {
            var request = new RestRequest($"/api/v1/seasons/{seasonId}", Method.Delete);
            AddAuthHeaders(request, AdminAccessToken);
            RestClient.Execute(request);
        }

        _seasonIdsToCleanUp.Clear();
    }

    protected void TrackSeasonForCleanup(string seasonId) => _seasonIdsToCleanUp.Add(seasonId);

    protected void UntrackSeason(string seasonId) => _seasonIdsToCleanUp.Remove(seasonId);
}
