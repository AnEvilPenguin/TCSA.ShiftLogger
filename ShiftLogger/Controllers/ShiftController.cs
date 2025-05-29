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
    public ActionResult<IEnumerable<ShiftDto>> GetAllShifts()
    {
        var output = _shiftService.GetShifts()
            .Select(s => s.ToDto());
        
        return Ok(output);
    }

    [HttpGet("{id:int}")]
    public ActionResult<ShiftDto> GetShift(int id)
    {
        var result = _shiftService.GetShiftById(id);
        
        if (result == null)
            return NotFound();
        
        return Ok(result?.ToDto());
    }

    [HttpPost]
    public ActionResult<ShiftDto> CreateShift(ShiftCreate shift)
    {
        try
        {
            var newShift = _shiftService.CreateShift(shift.ToShift());
            return Ok(newShift.ToDto());
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPut("{id:int}")]
    public ActionResult<Shift> UpdateShift(int id, ShiftUpdate shift)
    {
        try
        {
            var savedShift = _shiftService.UpdateShift(id, shift);
            
            if (savedShift == null)
                return NotFound();
            
            return Ok(savedShift.ToDto());
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult<Shift> DeleteShift(int id)
    {
        var result = _shiftService.DeleteShift(id);

        if (result == null)
            return NotFound();
        
        return Ok(result);
    }
}