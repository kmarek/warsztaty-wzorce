using FluentAssertions;
using RestSharp;
using System.Net;
using Testerzy.Trainings.Romanum.Framework.Api.Requests;
using Testerzy.Trainings.Romanum.Framework.Api.Responses;
using Testerzy.Trainings.Romanum.Framework.Configuration.Models;

namespace Testerzy.Trainings.Romanum.Api.Tests.Plays;

public class ListPlaysTests : PlayTestsBase
{
    [Test]
    public void Verify_PlaysListCanBeRetrieved()
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

        RestRequest listRequest = new("/api/v1/plays", Method.Get);
        listRequest.AddQueryParameter("page", "1");
        listRequest.AddQueryParameter("pageSize", "100");
        AddAuthHeaders(listRequest, AdminAccessToken);

        RestResponse<PagedResponse<PlayResponse>> listResponse = RestClient.Execute<PagedResponse<PlayResponse>>(listRequest);
        Console.WriteLine(listResponse.Content);
        listResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        PagedResponse<PlayResponse>? playList = listResponse.Data;
        playList.Should().NotBeNull();
        playList!.Meta.Page.Should().Be(1);
        playList.Meta.PageSize.Should().Be(100);
        playList.Meta.Total.Should().BeGreaterThanOrEqualTo(1);
        playList.Data.Should().Contain(p => p.Id == playId, "the just-created play should be part of the list");
    }
}
