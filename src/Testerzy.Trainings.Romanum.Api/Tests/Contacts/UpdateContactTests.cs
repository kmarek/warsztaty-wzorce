using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Contacts;

public class UpdateContactTests : BaseApiTest
{
    [Test]
    public void Verify_ContactCanBeUpdated()
    {
        var account = Settings.TestData.Accounts.First(u => u.Type == AccountType.Administrator);
        var tokens = OAuthTokenClient.GetTokenByPassword(account.Username, account.Password);

        RestRequest createRequest = new("/api/v1/contacts", Method.Post);
        PostContactRequest createBody = new()
        {
            Kind = "SCHOOL",
            FirstName = "John",
            LastName = "Doe",
            InstitutionName = "Acme Institution",
            Street = "Main Street",
            Email = "",
            AgeGroupIds = []
        };
        createRequest.AddJsonBody(createBody);
        AddAuthHeaders(createRequest, tokens.AccessToken);

        RestResponse<ContactResponse> createResponse = RestClient.Execute<ContactResponse>(createRequest);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        var contactId = createResponse.Data!.Id;

        RestRequest updateRequest = new($"/api/v1/contacts/{contactId}", Method.Patch);
        PostContactRequest updateBody = new()
        {
            Kind = "SCHOOL",
            FirstName = "Jane",
            LastName = "Smith",
            InstitutionName = "Updated Institution",
            Street = "Second Street",
            City = "Kraków",
            Email = "jane.smith@example.com",
            AgeGroupIds = []
        };
        updateRequest.AddJsonBody(updateBody);
        AddAuthHeaders(updateRequest, tokens.AccessToken);

        RestResponse<ContactResponse> updateResponse = RestClient.Execute<ContactResponse>(updateRequest);
        Console.WriteLine(updateResponse.Content);
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        ContactResponse? contact = updateResponse.Data;
        contact.Should().NotBeNull();
        contact!.Id.Should().Be(contactId, "updating a contact must not change its identity");
        contact.FirstName.Should().Be(updateBody.FirstName);
        contact.LastName.Should().Be(updateBody.LastName);
        contact.InstitutionName.Should().Be(updateBody.InstitutionName);
        contact.Street.Should().Be(updateBody.Street);
        contact.City.Should().Be(updateBody.City);
        contact.Email.Should().Be(updateBody.Email);
    }
}
