using Microsoft.AspNetCore.Mvc;
using ShiftLogger.Model;
using ShiftLogger.Services;

namespace ShiftLogger.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftController : ControllerBase
{
    private readonly IShiftService _shiftService;

    public ShiftController(IShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    [HttpGet]
    public IActionResult GetAllShifts()
    {
        return Ok(_shiftService.GetShifts());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetShift(int id)
    {
        return Ok(_shiftService.GetShiftById(id));
    }

    [HttpPost]
    public ActionResult<Shift> CreateShift(Shift shift)
    {
        return Ok(_shiftService.CreateShift(shift));
    }

    [HttpPut("{id:int}")]
    public ActionResult<Shift> UpdateShift(int id, Shift shift)
    {
        return Ok(_shiftService.UpdateShift(id, shift));
    }

    [HttpDelete("{id:int}")]
    public ActionResult<Shift> DeleteShift(int id)
    {
        var result = _shiftService.DeleteShift(id);

        if (result == null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }
}