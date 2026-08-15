using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Contacts;

public class DeleteContactTests : BaseApiTest
{
    [Test]
    public void Verify_ContactCanBeDeleted()
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

        RestRequest deleteRequest = new($"/api/v1/contacts/{contactId}", Method.Delete);
        AddAuthHeaders(deleteRequest, tokens.AccessToken);

        RestResponse deleteResponse = RestClient.Execute(deleteRequest);
        Console.WriteLine(deleteResponse.Content);
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        deleteResponse.Content.Should().BeNullOrEmpty();

        RestRequest getRequest = new($"/api/v1/contacts/{contactId}", Method.Get);
        AddAuthHeaders(getRequest, tokens.AccessToken);

        RestResponse getResponse = RestClient.Execute(getRequest);
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound, "a deleted contact must no longer be retrievable");
    }
}
