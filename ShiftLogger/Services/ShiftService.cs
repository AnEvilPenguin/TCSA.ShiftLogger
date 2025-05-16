using ShiftLogger.Data;
using ShiftLogger.Model;

namespace ShiftLogger.Services;

public interface IShiftService
{
    public List<Shift> GetShifts();
    public Shift? GetShiftById(int id);
    public Shift CreateShift(Shift shift);
    public Shift? UpdateShift(Shift shift);
    public string? DeleteShift(int id);
}

public class ShiftService : IShiftService
{
    private readonly ShiftsDbContext _dbContext;

    public ShiftService(ShiftsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Shift> GetShifts() => 
        _dbContext.Shifts.ToList();

    public Shift? GetShiftById(int id) =>
        _dbContext.Shifts.Find(id);
    
    public Shift CreateShift(Shift shift)
    {
        var savedShift = _dbContext.Shifts.Add(shift);
        _dbContext.SaveChanges();
        return savedShift.Entity;
    }

    public Shift? UpdateShift(Shift shift)
    {
        Shift? savedShift = GetShiftById(shift.Id);

        if (savedShift == null)
        {
            return null;
        }
        
        _dbContext.Entry(savedShift).CurrentValues.SetValues(shift);
        _dbContext.SaveChanges();

        return savedShift;
    }

    public string? DeleteShift(int id)
    {
        Shift? shift = GetShiftById(id);

        if (shift == null)
        {
            return null;
        }
        
        _dbContext.Shifts.Remove(shift);
        _dbContext.SaveChanges();
        return $"Successfully deleted shift with id {id}";
    }
}