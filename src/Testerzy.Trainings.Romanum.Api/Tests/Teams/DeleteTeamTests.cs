using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Teams;

public class DeleteTeamTests : TeamTestsBase
{
    [Test]
    public void Verify_TeamCanBeDeleted()
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

        RestRequest deleteRequest = new($"/api/v1/teams/{teamId}", Method.Delete);
        AddAuthHeaders(deleteRequest, AdminAccessToken);

        RestResponse deleteResponse = RestClient.Execute(deleteRequest);
        Console.WriteLine(deleteResponse.Content);
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        deleteResponse.Content.Should().BeNullOrEmpty();
        UntrackTeam(teamId);

        RestRequest getRequest = new($"/api/v1/teams/{teamId}", Method.Get);
        AddAuthHeaders(getRequest, AdminAccessToken);

        RestResponse getResponse = RestClient.Execute(getRequest);
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound, "a deleted team must no longer be retrievable");
    }
}
