using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Seasons;

public class DeleteSeasonTests : SeasonTestsBase
{
    [Test]
    public void Verify_SeasonCanBeDeleted()
    {
        RestRequest createRequest = new("/api/v1/seasons", Method.Post);
        SeasonRequest createBody = new()
        {
            Name = $"QA Season {Guid.NewGuid():N}",
            StartDate = "2026-01-01T00:00:00.000Z",
            EndDate = "2026-06-30T00:00:00.000Z"
        };
        createRequest.AddJsonBody(createBody);
        AddAuthHeaders(createRequest, AdminAccessToken);

        RestResponse<SeasonResponse> createResponse = RestClient.Execute<SeasonResponse>(createRequest);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        var seasonId = createResponse.Data!.Id;
        TrackSeasonForCleanup(seasonId);

        RestRequest deleteRequest = new($"/api/v1/seasons/{seasonId}", Method.Delete);
        AddAuthHeaders(deleteRequest, AdminAccessToken);

        RestResponse deleteResponse = RestClient.Execute(deleteRequest);
        Console.WriteLine(deleteResponse.Content);
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        deleteResponse.Content.Should().BeNullOrEmpty();
        UntrackSeason(seasonId);

        RestRequest getRequest = new($"/api/v1/seasons/{seasonId}", Method.Get);
        AddAuthHeaders(getRequest, AdminAccessToken);

        RestResponse getResponse = RestClient.Execute(getRequest);
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound, "a deleted season must no longer be retrievable");
    }
}
