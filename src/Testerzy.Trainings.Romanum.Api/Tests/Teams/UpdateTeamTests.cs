using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Teams;

public class UpdateTeamTests : TeamTestsBase
{
    [Test]
    public void Verify_TeamCanBeUpdated()
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

        RestRequest updateRequest = new($"/api/v1/teams/{teamId}", Method.Patch);
        TeamRequest updateBody = new()
        {
            Name = "Musical Ensemble",
            Description = "The resident musical theatre ensemble"
        };
        updateRequest.AddJsonBody(updateBody);
        AddAuthHeaders(updateRequest, AdminAccessToken);

        RestResponse<TeamResponse> updateResponse = RestClient.Execute<TeamResponse>(updateRequest);
        Console.WriteLine(updateResponse.Content);
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        TeamResponse? team = updateResponse.Data;
        team.Should().NotBeNull();
        team!.Id.Should().Be(teamId, "updating a team must not change its identity");
        team.Name.Should().Be(updateBody.Name);
        team.Description.Should().Be(updateBody.Description);
    }
}
