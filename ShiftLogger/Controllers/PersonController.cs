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

    [HttpGet("{id:int}/shifts")]
    public ActionResult<PersonShiftDTO>? GetPersonShifts(int id)
    {
        var result = _personService.GetPersonById(id, true);

        if (result == null)
            return null;

        var shifts = result.Shifts.Select(s => new ShiftTransfer()
        {
            Id = s.Id,
            Start = s.Start,
            End = s.End
        });

        var output = new PersonShiftDTO()
        {
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Shifts = shifts
        };

        return Ok(output);
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
