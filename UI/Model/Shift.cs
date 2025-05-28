using System.Text.Json.Serialization;

namespace UI.Model;

public class Shift
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("start")]
    public required DateTime Start { get; init; }
    [JsonPropertyName("end")]
    public required DateTime End { get; init; }
    
    public string GetDuration()
    {
        var duration = End - Start;
        
        return duration.ToString(@"d\ \-\ hh\:mm\:ss");
    }
}