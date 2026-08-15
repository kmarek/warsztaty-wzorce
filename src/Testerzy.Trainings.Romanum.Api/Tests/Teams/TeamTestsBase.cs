using RestSharp;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Teams;

public abstract class TeamTestsBase : BaseApiTest
{
    private readonly List<string> _teamIdsToCleanUp = [];

    [TearDown]
    public void TeamTestsTearDown()
    {
        if (_teamIdsToCleanUp.Count == 0)
        {
            return;
        }

        foreach (var teamId in _teamIdsToCleanUp)
        {
            var request = new RestRequest($"/api/v1/teams/{teamId}", Method.Delete);
            AddAuthHeaders(request, AdminAccessToken);
            RestClient.Execute(request);
        }

        _teamIdsToCleanUp.Clear();
    }

    protected void TrackTeamForCleanup(string teamId) => _teamIdsToCleanUp.Add(teamId);

    protected void UntrackTeam(string teamId) => _teamIdsToCleanUp.Remove(teamId);
}
