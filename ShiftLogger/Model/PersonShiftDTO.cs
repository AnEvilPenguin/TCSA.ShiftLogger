namespace ShiftLogger.Model;

public class PersonShiftDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public IEnumerable<ShiftTransfer> Shifts { get; set; } = [];
}

public class ShiftTransfer
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}


