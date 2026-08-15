using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Teams;

public class ListTeamsTests : TeamTestsBase
{
    [Test]
    public void Verify_TeamsListCanBeRetrieved()
    {
        RestRequest createRequest = new("/api/v1/teams", Method.Post);
        TeamRequest createBody = new()
        {
            Name = "Drama Ensemble",
            Description = "The resident acting ensemble"
        };
        createRequest.AddJsonBody(createBody);
        AddAuthHeaders(createRequest, AdminAccessToken);

        RestResponse<TeamResponse> createResponse = RestClient.Execute<TeamResponse>(createRequest);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        var teamId = createResponse.Data!.Id;
        TrackTeamForCleanup(teamId);

        RestRequest listRequest = new("/api/v1/teams", Method.Get);
        listRequest.AddQueryParameter("page", "1");
        listRequest.AddQueryParameter("pageSize", "100");
        AddAuthHeaders(listRequest, AdminAccessToken);

        RestResponse<PagedResponse<TeamResponse>> listResponse = RestClient.Execute<PagedResponse<TeamResponse>>(listRequest);
        Console.WriteLine(listResponse.Content);
        listResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        PagedResponse<TeamResponse>? teamList = listResponse.Data;
        teamList.Should().NotBeNull();
        teamList!.Meta.Page.Should().Be(1);
        teamList.Meta.PageSize.Should().Be(100);
        teamList.Meta.Total.Should().BeGreaterThanOrEqualTo(1);
        teamList.Data.Should().Contain(t => t.Id == teamId, "the just-created team should be part of the list");
    }
}
