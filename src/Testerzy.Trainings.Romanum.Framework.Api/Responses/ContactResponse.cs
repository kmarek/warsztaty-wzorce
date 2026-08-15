using System.Text.Json.Serialization;

namespace Testerzy.Trainings.Romanum.Framework.Api.Responses;

public sealed class ContactResponse
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = default!;

    [JsonPropertyName("kind")]
    public string Kind { get; init; } = default!;

    [JsonPropertyName("firstName")]
    public string FirstName { get; init; } = default!;

    [JsonPropertyName("lastName")]
    public string LastName { get; init; } = default!;

    [JsonPropertyName("institutionName")]
    public string InstitutionName { get; init; } = default!;

    [JsonPropertyName("acronym")]
    public string? Acronym { get; init; }

    [JsonPropertyName("street")]
    public string Street { get; init; } = default!;

    [JsonPropertyName("streetNumber")]
    public string? StreetNumber { get; init; }

    [JsonPropertyName("phone")]
    public string? Phone { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("email2")]
    public string? Email2 { get; init; }

    [JsonPropertyName("postalCode")]
    public string? PostalCode { get; init; }

    [JsonPropertyName("city")]
    public string? City { get; init; }

    [JsonPropertyName("position")]
    public string? Position { get; init; }

    [JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; init; }

    [JsonPropertyName("updatedAt")]
    public DateTimeOffset UpdatedAt { get; init; }
}
