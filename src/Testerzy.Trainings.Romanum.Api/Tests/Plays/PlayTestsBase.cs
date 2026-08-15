using RestSharp;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Plays;

public abstract class PlayTestsBase : BaseApiTest
{
    private readonly List<string> _playIdsToCleanUp = [];

    [TearDown]
    public void PlayTestsTearDown()
    {
        if (_playIdsToCleanUp.Count == 0)
        {
            return;
        }

        foreach (var playId in _playIdsToCleanUp)
        {
            var request = new RestRequest($"/api/v1/plays/{playId}", Method.Delete);
            AddAuthHeaders(request, AdminAccessToken);
            RestClient.Execute(request);
        }

        _playIdsToCleanUp.Clear();
    }

    protected void TrackPlayForCleanup(string playId) => _playIdsToCleanUp.Add(playId);

    protected void UntrackPlay(string playId) => _playIdsToCleanUp.Remove(playId);
}
