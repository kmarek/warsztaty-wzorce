using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Seasons;

public class AddSeasonTests : SeasonTestsBase
{
    [Test]
    public void Verify_SeasonCanBeAdded()
    {
        RestRequest request = new("/api/v1/seasons", Method.Post);
        SeasonRequest requestBody = new()
        {
            Name = $"QA Season {Guid.NewGuid():N}",
            StartDate = "2026-01-01T00:00:00.000Z",
            EndDate = "2026-06-30T00:00:00.000Z"
        };
        request.AddJsonBody(requestBody);
        AddAuthHeaders(request, AdminAccessToken);

        RestResponse<SeasonResponse> response = RestClient.Execute<SeasonResponse>(request);
        Console.WriteLine(response.Content);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        SeasonResponse? season = response.Data;
        season.Should().NotBeNull();
        season!.Id.Should().NotBeNullOrEmpty();
        TrackSeasonForCleanup(season.Id);
        season.Name.Should().Be(requestBody.Name);
        season.StartDate.Should().Be(DateTimeOffset.Parse(requestBody.StartDate));
        season.EndDate.Should().Be(DateTimeOffset.Parse(requestBody.EndDate));
        season.IsCurrent.Should().BeFalse("a newly created season must not become current automatically");
    }
}
