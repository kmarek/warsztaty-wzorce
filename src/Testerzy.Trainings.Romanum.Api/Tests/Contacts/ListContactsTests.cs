using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Contacts;

public class ListContactsTests : BaseApiTest
{
    [Test]
    public void Verify_ContactsListCanBeRetrieved()
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

        RestRequest listRequest = new("/api/v1/contacts", Method.Get);
        listRequest.AddQueryParameter("page", "1");
        listRequest.AddQueryParameter("pageSize", "100");
        AddAuthHeaders(listRequest, tokens.AccessToken);

        RestResponse<ContactListResponse> listResponse = RestClient.Execute<ContactListResponse>(listRequest);
        Console.WriteLine(listResponse.Content);
        listResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        ContactListResponse? contactList = listResponse.Data;
        contactList.Should().NotBeNull();
        contactList!.Meta.Page.Should().Be(1);
        contactList.Meta.PageSize.Should().Be(100);
        contactList.Meta.Total.Should().BeGreaterThanOrEqualTo(1);
        contactList.Data.Should().Contain(c => c.Id == contactId, "the just-created contact should be part of the list");
    }
}
