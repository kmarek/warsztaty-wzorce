using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Plays;

public class DeletePlayTests : PlayTestsBase
{
    [Test]
    public void Verify_PlayCanBeDeleted()
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

        RestRequest deleteRequest = new($"/api/v1/plays/{playId}", Method.Delete);
        AddAuthHeaders(deleteRequest, AdminAccessToken);

        RestResponse deleteResponse = RestClient.Execute(deleteRequest);
        Console.WriteLine(deleteResponse.Content);
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        deleteResponse.Content.Should().BeNullOrEmpty();
        UntrackPlay(playId);

        RestRequest getRequest = new($"/api/v1/plays/{playId}", Method.Get);
        AddAuthHeaders(getRequest, AdminAccessToken);

        RestResponse getResponse = RestClient.Execute(getRequest);
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound, "a deleted play must no longer be retrievable");
    }
}
