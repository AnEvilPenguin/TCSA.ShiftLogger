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
        try
        {
            await using Stream stream = await _client.GetStreamAsync("Person");

            var people = await JsonSerializer.DeserializeAsync<List<Person>>(stream);

            if (people == null)
                return new List<Person>();

            return people;
        }
        catch
        {
            return new List<Person>();
        }
    }

    public async Task<Person?> AddPerson(string firstName, string lastName)
    {
        try
        {
            var response = await _client.PostAsJsonAsync("Person", new { firstName, lastName });

            return await response.Content.ReadFromJsonAsync<Person>();
        }
        catch
        {
            return null;
        }
    }

    public async Task<Person?> UpdatePerson(Person person)
    {
        try
        {
            var response = await _client.PutAsJsonAsync($"Person/{person.Id}", person);

            return await response.Content.ReadFromJsonAsync<Person>();
        }
        catch
        {
            return null;
        }
    }

    public async Task<string?> DeletePerson(Person person)
    {
        try
        {
            var response = await _client.DeleteAsync($"Person/{person.Id}");

            await using Stream stream = await response.Content.ReadAsStreamAsync();

            using var reader = new StreamReader(stream);

            var responseText = await reader.ReadToEndAsync();

            return responseText;
        }
        catch
        {
            return null;
        }
    }
}