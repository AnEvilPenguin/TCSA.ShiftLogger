using System.Text.Json.Serialization;

namespace UI.Model;

public record PersonShiftDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("firstName")]
    public required string FirstName { get; init; }
    [JsonPropertyName("lastName")]
    public required string LastName { get; init; }
    [JsonPropertyName("shifts")]
    public required List<Shift> Shifts { get; init; }
}