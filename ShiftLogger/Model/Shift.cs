namespace ShiftLogger.Model;

public class Shift
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int PersonId { get; set; }
    public Person? Person { get; set; }
}

public class ShiftTransfer
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}

