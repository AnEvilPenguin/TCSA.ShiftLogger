namespace ShiftLogger.Model;

public class PersonShiftTransfer
{
    public required PersonTransfer Person { get; set; }
    public List<ShiftTransfer> Sessions { get; set; } = [];
}

