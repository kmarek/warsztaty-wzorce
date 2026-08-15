using System.Text.Json.Serialization;

namespace Testerzy.Trainings.Romanum.Framework.Api.Responses;

public sealed class PagedResponse<T>
{
    [JsonPropertyName("data")]
    public List<T> Data { get; init; } = [];

    [JsonPropertyName("meta")]
    public ListMeta Meta { get; init; } = default!;
}
