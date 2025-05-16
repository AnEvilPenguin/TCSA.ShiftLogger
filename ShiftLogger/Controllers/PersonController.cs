using Microsoft.AspNetCore.Mvc;
using ShiftLogger.Model;
using ShiftLogger.Services;

namespace ShiftLogger.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    
    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public ActionResult<List<Person>> GetAllPeople()
    {
        return Ok(_personService.GetPeople());
    }
    
    [HttpGet("{id:int}")]
    public ActionResult<Person> GetPersonById(int id)
    {
        return Ok(_personService.GetPersonById(id));
    }

    [HttpGet("{id:int}")]
    public ActionResult<Person> GetPersonShifts(int id)
    {
        throw new NotImplementedException();
        // FIXME some sort of DTO
    }

    [HttpPost]
    public ActionResult<Person> CreatePerson(Person person)
    {
        return Ok(_personService.CreatePerson(person));
    }

    [HttpPut("{id:int}")]
    public ActionResult<Person> UpdatePerson(int id, Person person)
    {
        var result = _personService.UpdatePerson(id, person);

        if (result == null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<Person> DeletePerson(int id)
    {
        var result = _personService.DeletePerson(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}