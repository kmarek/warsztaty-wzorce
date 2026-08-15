using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Venues;

public class AddVenueTests : VenueTestsBase
{
    [Test]
    public void Verify_VenueCanBeAdded()
    {
        RestRequest request = new("/api/v1/venues", Method.Post);
        VenueRequest requestBody = new()
        {
            Name = "Main Hall",
            Address = "Main Street 1"
        };
        request.AddJsonBody(requestBody);
        AddAuthHeaders(request, AdminAccessToken);

        RestResponse<VenueResponse> response = RestClient.Execute<VenueResponse>(request);
        Console.WriteLine(response.Content);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        VenueResponse? venue = response.Data;
        venue.Should().NotBeNull();
        venue!.Id.Should().NotBeNullOrEmpty();
        TrackVenueForCleanup(venue.Id);
        venue.Name.Should().Be(requestBody.Name);
        venue.Address.Should().Be(requestBody.Address);
        venue.ContactId.Should().BeNull("no contact was linked when creating the venue");
    }
}
