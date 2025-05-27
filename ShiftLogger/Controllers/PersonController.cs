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
    public ActionResult<IEnumerable<PersonDto>> GetAllPeople()
    {
        return Ok(_personService.GetPeople().Select(p => p.ToDto()));
    }

    [HttpGet("{id:int}")]
    public ActionResult<PersonDto> GetPersonById(int id)
    {
        var person = _personService.GetPersonById(id);
        
        if (person == null)
            return NotFound();
        
        return Ok(person.ToDto());
    }

    [HttpGet("{id:int}/shifts")]
    public ActionResult<PersonShiftDTO>? GetPersonShifts(int id)
    {
        var result = _personService.GetPersonById(id, true);

        if (result == null)
            return NotFound();

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
    public ActionResult<PersonDto> CreatePerson(PersonNameOnly dto)
    {
        var savedPerson = _personService.CreatePerson(dto.ToPerson());
        return Ok(savedPerson.ToDto());
    }

    [HttpPut("{id:int}")]
    public ActionResult<PersonDto> UpdatePerson(int id, PersonNameOnly person)
    {
        var result = _personService.UpdatePerson(id, person);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result.ToDto());
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
