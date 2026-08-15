using RestSharp;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Contacts;

public abstract class ContactTestsBase : BaseApiTest
{
    private readonly List<string> _contactIdsToCleanUp = [];

    [TearDown]
    public void ContactTestsTearDown()
    {
        if (_contactIdsToCleanUp.Count == 0)
        {
            return;
        }

        foreach (var contactId in _contactIdsToCleanUp)
        {
            var request = new RestRequest($"/api/v1/contacts/{contactId}", Method.Delete);
            AddAuthHeaders(request, AdminAccessToken);
            RestClient.Execute(request);
        }

        _contactIdsToCleanUp.Clear();
    }

    protected void TrackContactForCleanup(string contactId) => _contactIdsToCleanUp.Add(contactId);

    protected void UntrackContact(string contactId) => _contactIdsToCleanUp.Remove(contactId);
}
