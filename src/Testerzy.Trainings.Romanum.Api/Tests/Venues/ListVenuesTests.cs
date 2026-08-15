using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Venues;

public class ListVenuesTests : VenueTestsBase
{
    [Test]
    public void Verify_VenuesListCanBeRetrieved()
    {
        RestRequest createRequest = new("/api/v1/venues", Method.Post);
        VenueRequest createBody = new()
        {
            Name = "Main Hall",
            Address = "Main Street 1"
        };
        createRequest.AddJsonBody(createBody);
        AddAuthHeaders(createRequest, AdminAccessToken);

        RestResponse<VenueResponse> createResponse = RestClient.Execute<VenueResponse>(createRequest);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        var venueId = createResponse.Data!.Id;
        TrackVenueForCleanup(venueId);

        RestRequest listRequest = new("/api/v1/venues", Method.Get);
        listRequest.AddQueryParameter("page", "1");
        listRequest.AddQueryParameter("pageSize", "100");
        AddAuthHeaders(listRequest, AdminAccessToken);

        RestResponse<PagedResponse<VenueResponse>> listResponse = RestClient.Execute<PagedResponse<VenueResponse>>(listRequest);
        Console.WriteLine(listResponse.Content);
        listResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        PagedResponse<VenueResponse>? venueList = listResponse.Data;
        venueList.Should().NotBeNull();
        venueList!.Meta.Page.Should().Be(1);
        venueList.Meta.PageSize.Should().Be(100);
        venueList.Meta.Total.Should().BeGreaterThanOrEqualTo(1);
        venueList.Data.Should().Contain(v => v.Id == venueId, "the just-created venue should be part of the list");
    }
}
