using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Venues;

public class DeleteVenueTests : VenueTestsBase
{
    [Test]
    public void Verify_VenueCanBeDeleted()
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

        RestRequest deleteRequest = new($"/api/v1/venues/{venueId}", Method.Delete);
        AddAuthHeaders(deleteRequest, AdminAccessToken);

        RestResponse deleteResponse = RestClient.Execute(deleteRequest);
        Console.WriteLine(deleteResponse.Content);
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        deleteResponse.Content.Should().BeNullOrEmpty();
        UntrackVenue(venueId);

        RestRequest getRequest = new($"/api/v1/venues/{venueId}", Method.Get);
        AddAuthHeaders(getRequest, AdminAccessToken);

        RestResponse getResponse = RestClient.Execute(getRequest);
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound, "a deleted venue must no longer be retrievable");
    }
}
