using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Contacts;

public class ListContactsTests : ContactTestsBase
{
    [Test]
    public void Verify_ContactsListCanBeRetrieved()
    {
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
        AddAuthHeaders(createRequest, AdminAccessToken);

        RestResponse<ContactResponse> createResponse = RestClient.Execute<ContactResponse>(createRequest);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        var contactId = createResponse.Data!.Id;
        TrackContactForCleanup(contactId);

        RestRequest listRequest = new("/api/v1/contacts", Method.Get);
        listRequest.AddQueryParameter("page", "1");
        listRequest.AddQueryParameter("pageSize", "100");
        AddAuthHeaders(listRequest, AdminAccessToken);

        RestResponse<PagedResponse<ContactResponse>> listResponse = RestClient.Execute<PagedResponse<ContactResponse>>(listRequest);
        Console.WriteLine(listResponse.Content);
        listResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        PagedResponse<ContactResponse>? contactList = listResponse.Data;
        contactList.Should().NotBeNull();
        contactList!.Meta.Page.Should().Be(1);
        contactList.Meta.PageSize.Should().Be(100);
        contactList.Meta.Total.Should().BeGreaterThanOrEqualTo(1);
        contactList.Data.Should().Contain(c => c.Id == contactId, "the just-created contact should be part of the list");
    }
}
