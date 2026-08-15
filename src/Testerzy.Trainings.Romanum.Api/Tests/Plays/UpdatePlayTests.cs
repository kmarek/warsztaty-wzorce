using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Plays;

public class UpdatePlayTests : PlayTestsBase
{
    [Test]
    public void Verify_PlayCanBeUpdated()
    {
        RestRequest createRequest = new("/api/v1/plays", Method.Post);
        PlayRequest createBody = new()
        {
            Name = "Hamlet",
            Description = "Tragedy by William Shakespeare"
        };
        createRequest.AddJsonBody(createBody);
        AddAuthHeaders(createRequest, AdminAccessToken);

        RestResponse<PlayResponse> createResponse = RestClient.Execute<PlayResponse>(createRequest);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        var playId = createResponse.Data!.Id;
        TrackPlayForCleanup(playId);

        RestRequest updateRequest = new($"/api/v1/plays/{playId}", Method.Patch);
        PlayRequest updateBody = new()
        {
            Name = "Macbeth",
            Description = "Another tragedy by William Shakespeare"
        };
        updateRequest.AddJsonBody(updateBody);
        AddAuthHeaders(updateRequest, AdminAccessToken);

        RestResponse<PlayResponse> updateResponse = RestClient.Execute<PlayResponse>(updateRequest);
        Console.WriteLine(updateResponse.Content);
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        PlayResponse? play = updateResponse.Data;
        play.Should().NotBeNull();
        play!.Id.Should().Be(playId, "updating a play must not change its identity");
        play.Name.Should().Be(updateBody.Name);
        play.Description.Should().Be(updateBody.Description);
    }
}
