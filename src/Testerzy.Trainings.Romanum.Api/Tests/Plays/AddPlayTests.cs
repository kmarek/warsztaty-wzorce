using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Plays;

public class AddPlayTests : PlayTestsBase
{
    [Test]
    public void Verify_PlayCanBeAdded()
    {
        RestRequest request = new("/api/v1/plays", Method.Post);
        PlayRequest requestBody = new()
        {
            Name = "Hamlet",
            Description = "Tragedy by William Shakespeare"
        };
        request.AddJsonBody(requestBody);
        AddAuthHeaders(request, AdminAccessToken);

        RestResponse<PlayResponse> response = RestClient.Execute<PlayResponse>(request);
        Console.WriteLine(response.Content);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        PlayResponse? play = response.Data;
        play.Should().NotBeNull();
        play!.Id.Should().NotBeNullOrEmpty();
        TrackPlayForCleanup(play.Id);
        play.Name.Should().Be(requestBody.Name);
        play.Description.Should().Be(requestBody.Description);
        play.AgeGroupId.Should().BeNull();
        play.TeamId.Should().BeNull();
    }
}
