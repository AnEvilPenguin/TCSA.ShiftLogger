namespace ShiftLogger.Model;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public ICollection<Shift> Shifts { get; set; } = [];

    public PersonDto ToDto() => new PersonDto()
    {
        Id = Id,
        FirstName = FirstName,
        LastName = LastName,
    };
}

public class PersonDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public Person ToPerson() => new Person()
    {
        Id = Id,
        FirstName = FirstName,
        LastName = LastName,
    };
}

public class PersonNameOnly
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public Person ToPerson() => new Person()
    {
        FirstName = FirstName,
        LastName = LastName,
    };
}
