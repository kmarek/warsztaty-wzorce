using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Seasons;

public class UpdateSeasonTests : SeasonTestsBase
{
    [Test]
    public void Verify_SeasonCanBeUpdated()
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

        RestRequest updateRequest = new($"/api/v1/seasons/{seasonId}", Method.Patch);
        SeasonRequest updateBody = new()
        {
            Name = $"QA Season {Guid.NewGuid():N}",
            StartDate = "2026-07-01T00:00:00.000Z",
            EndDate = "2026-12-31T00:00:00.000Z"
        };
        updateRequest.AddJsonBody(updateBody);
        AddAuthHeaders(updateRequest, AdminAccessToken);

        RestResponse<SeasonResponse> updateResponse = RestClient.Execute<SeasonResponse>(updateRequest);
        Console.WriteLine(updateResponse.Content);
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        SeasonResponse? season = updateResponse.Data;
        season.Should().NotBeNull();
        season!.Id.Should().Be(seasonId, "updating a season must not change its identity");
        season.Name.Should().Be(updateBody.Name);
        season.StartDate.Should().Be(DateTimeOffset.Parse(updateBody.StartDate));
        season.EndDate.Should().Be(DateTimeOffset.Parse(updateBody.EndDate));
    }
}
