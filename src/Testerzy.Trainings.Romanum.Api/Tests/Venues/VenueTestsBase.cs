using RestSharp;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Venues;

public abstract class VenueTestsBase : BaseApiTest
{
    private readonly List<string> _venueIdsToCleanUp = [];

    [TearDown]
    public void VenueTestsTearDown()
    {
        if (_venueIdsToCleanUp.Count == 0)
        {
            return;
        }

        foreach (var venueId in _venueIdsToCleanUp)
        {
            var request = new RestRequest($"/api/v1/venues/{venueId}", Method.Delete);
            AddAuthHeaders(request, AdminAccessToken);
            RestClient.Execute(request);
        }

        _venueIdsToCleanUp.Clear();
    }

    protected void TrackVenueForCleanup(string venueId) => _venueIdsToCleanUp.Add(venueId);

    protected void UntrackVenue(string venueId) => _venueIdsToCleanUp.Remove(venueId);
}
