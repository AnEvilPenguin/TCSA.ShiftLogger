using System.Net.Http.Json;
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
        await using var stream = await _client.GetStreamAsync($"Person/{personId}/shifts");
        
        var person = await JsonSerializer.DeserializeAsync<PersonShiftDto>(stream);
        
        if (person == null)
            return new List<Shift>();
        
        return person.Shifts;
    }
    
    public async Task<Shift?> AddShift(int personId, DateTime start, DateTime end)
    {
        var content = new
        {
            personId,
            start = start.ToString("s"),
            end = end.ToString("s"),
        };
        
        var response = await _client.PostAsJsonAsync("Shift", content);
        
        var shift = await response.Content.ReadFromJsonAsync<Shift>();
        
        return shift;
    }

    public async Task<string?> RemoveShift(Shift shift)
    {
        try
        {
            var response = await _client.DeleteAsync($"Shift/{shift.Id}");

            return await response.Content.ReadAsStringAsync();
        }
        catch
        {
            return null;
        }
    }

    public async Task<Shift?> UpdateShift(Shift shift)
    {
        var content = new
        {
            start = shift.Start.ToString("s"),
            end = shift.End.ToString("s"),
            personId = shift.PersonId,
        };
        
        try
        {
            var response = await _client.PutAsJsonAsync($"Shift/{shift.Id}", content);

            return await response.Content.ReadFromJsonAsync<Shift>();
        }
        catch
        {
            return null;
        }
    }
}