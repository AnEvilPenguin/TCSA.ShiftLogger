using System.Text.Json.Serialization;

namespace UI.Model;

public class Shift
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("start")]
    public required DateTime Start { get; init; }
    [JsonPropertyName("end")]
    public DateTime? End { get; init; }
}