using System.Text.Json;
using UI.Model;

namespace UI.Controllers;

public class ShiftController(string baseUrl)
{
    private readonly HttpClient _client = new ()
    {
        BaseAddress = new Uri(baseUrl)
    };

    public async Task<List<Shift>> GetShifts(int personId)
    {
        await using var stream = await _client.GetStreamAsync($"/Person/{personId}/shifts");
        
        var person = await JsonSerializer.DeserializeAsync<PersonShiftDTO>(stream);
        
        if (person == null)
            return new List<Shift>();
        
        return person.Shifts;
    }
}