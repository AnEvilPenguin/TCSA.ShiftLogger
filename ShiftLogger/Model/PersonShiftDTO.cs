namespace ShiftLogger.Model;

public class PersonShiftDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public IEnumerable<ShiftTransfer> Shifts { get; set; } = [];
}

