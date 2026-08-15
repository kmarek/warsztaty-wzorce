using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Seasons;

public class ListSeasonsTests : SeasonTestsBase
{
    [Test]
    public void Verify_SeasonsListCanBeRetrieved()
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

        RestRequest listRequest = new("/api/v1/seasons", Method.Get);
        listRequest.AddQueryParameter("page", "1");
        listRequest.AddQueryParameter("pageSize", "100");
        AddAuthHeaders(listRequest, AdminAccessToken);

        RestResponse<PagedResponse<SeasonResponse>> listResponse = RestClient.Execute<PagedResponse<SeasonResponse>>(listRequest);
        Console.WriteLine(listResponse.Content);
        listResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        PagedResponse<SeasonResponse>? seasonList = listResponse.Data;
        seasonList.Should().NotBeNull();
        seasonList!.Meta.Page.Should().Be(1);
        seasonList.Meta.PageSize.Should().Be(100);
        seasonList.Meta.Total.Should().BeGreaterThanOrEqualTo(1);
        seasonList.Data.Should().Contain(s => s.Id == seasonId, "the just-created season should be part of the list");
    }
}
