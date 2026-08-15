using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Teams;

public class AddTeamTests : TeamTestsBase
{
    [Test]
    public void Verify_TeamCanBeAdded()
    {
        RestRequest request = new("/api/v1/teams", Method.Post);
        TeamRequest requestBody = new()
        {
            Name = "Drama Ensemble",
            Description = "The resident acting ensemble"
        };
        request.AddJsonBody(requestBody);
        AddAuthHeaders(request, AdminAccessToken);

        RestResponse<TeamResponse> response = RestClient.Execute<TeamResponse>(request);
        Console.WriteLine(response.Content);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        TeamResponse? team = response.Data;
        team.Should().NotBeNull();
        team!.Id.Should().NotBeNullOrEmpty();
        TrackTeamForCleanup(team.Id);
        team.Name.Should().Be(requestBody.Name);
        team.Description.Should().Be(requestBody.Description);
    }
}
