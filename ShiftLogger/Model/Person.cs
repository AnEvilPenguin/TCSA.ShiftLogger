namespace ShiftLogger.Model;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}