namespace ShiftLogger.Model;

public class Shift
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int PersonId { get; set; }
    public Person? Person { get; set; }

    public ShiftDto ToDto() => new ShiftDto()
    {
        Id = Id,
        Start = Start,
        End = End,
        PersonId = PersonId,
    };
}

public class ShiftDto
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int PersonId { get; set; }

    public Shift ToShift() => new Shift()
    {
        Id = Id,
        Start = Start,
        End = End,
        PersonId = PersonId,
    };
}

public class ShiftCreate
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int PersonId { get; set; }

    public Shift ToShift() => new Shift()
    {
        Start = Start,
        End = End,
        PersonId = PersonId,
    };
}

public class ShiftUpdate
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}

