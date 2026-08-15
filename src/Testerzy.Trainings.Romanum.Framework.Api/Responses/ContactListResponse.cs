using System.Text.Json.Serialization;

namespace Testerzy.Trainings.Romanum.Framework.Api.Responses;

public sealed class ContactListResponse
{
    [JsonPropertyName("data")]
    public List<ContactResponse> Data { get; init; } = [];

    [JsonPropertyName("meta")]
    public ContactListMeta Meta { get; init; } = default!;
}
