using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Venues;

public class UpdateVenueTests : VenueTestsBase
{
    [Test]
    public void Verify_VenueCanBeUpdated()
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

        RestRequest updateRequest = new($"/api/v1/venues/{venueId}", Method.Patch);
        VenueRequest updateBody = new()
        {
            Name = "Updated Hall",
            Address = "Second Street 2"
        };
        updateRequest.AddJsonBody(updateBody);
        AddAuthHeaders(updateRequest, AdminAccessToken);

        RestResponse<VenueResponse> updateResponse = RestClient.Execute<VenueResponse>(updateRequest);
        Console.WriteLine(updateResponse.Content);
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        VenueResponse? venue = updateResponse.Data;
        venue.Should().NotBeNull();
        venue!.Id.Should().Be(venueId, "updating a venue must not change its identity");
        venue.Name.Should().Be(updateBody.Name);
        venue.Address.Should().Be(updateBody.Address);
    }
}
