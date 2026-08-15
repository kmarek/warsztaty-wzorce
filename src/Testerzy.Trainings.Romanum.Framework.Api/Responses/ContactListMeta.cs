using System.Text.Json.Serialization;

namespace Testerzy.Trainings.Romanum.Framework.Api.Responses;

public sealed class ContactListMeta
{
    [JsonPropertyName("page")]
    public int Page { get; init; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; init; }

    [JsonPropertyName("total")]
    public int Total { get; init; }
}
