using System.Net.Http.Json;
using System.Text.Json;
using UI.Model;

namespace UI.Controllers;

public class PersonController(string baseUrl)
{
    private readonly HttpClient _client = new ()
    {
        BaseAddress = new Uri(baseUrl)
    };

    public async Task<List<Person>> ListPeople()
    {
        await using Stream stream = await _client.GetStreamAsync("Person");
        
        var people = await JsonSerializer.DeserializeAsync<List<Person>>(stream);
        
        if (people == null)
            return new List<Person>();

        return people;
    }

    public async Task<Person?> AddPerson(string firstName, string lastName)
    {
        var response = await _client.PostAsJsonAsync("Person", new {firstName, lastName});
        
        var person = await response.Content.ReadFromJsonAsync<Person>();
        
        return person;
    }

    public async Task<Person?> UpdatePerson(Person person)
    {
        var response = await _client.PutAsJsonAsync($"Person/{person.Id}", person);
        
        return await response.Content.ReadFromJsonAsync<Person>();
    }

    public async Task<string> DeletePerson(Person person)
    {
        var response = await _client.DeleteAsync($"Person/{person.Id}");
        
        await using Stream stream = await response.Content.ReadAsStreamAsync();
        
        using var reader = new StreamReader(stream);

        var responseText = await reader.ReadToEndAsync();
        
        return responseText;
    }
}