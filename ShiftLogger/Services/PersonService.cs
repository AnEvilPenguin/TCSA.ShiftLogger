using Microsoft.EntityFrameworkCore;
using ShiftLogger.Data;
using ShiftLogger.Model;

namespace ShiftLogger.Services;

public interface IPersonService
{
    public List<Person> GetPeople();
    public Person? GetPersonById(int id, bool includeShifts = false);
    public Person CreatePerson(Person person);
    public Person? UpdatePerson(Person person);
    public string? DeletePerson(int id);
}

public class PersonService : IPersonService
{
    private readonly ShiftsDbContext _dbContext;

    public PersonService(ShiftsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public List<Person> GetPeople() => 
        _dbContext.People.ToList();

    public Person? GetPersonById(int id, bool includeShifts = false)
    {
        if  (includeShifts)
            return _dbContext.People
                .Include(p => p.Shifts)
                .Single(p => p.Id == id);

        return _dbContext.People.Find(id);
    }
        

    public Person CreatePerson(Person person)
    {
        var savedPerson = _dbContext.People.Add(person);
        _dbContext.SaveChanges();
        return savedPerson.Entity;
    }

    public Person? UpdatePerson(Person person)
    {
        Person? savedPerson = GetPersonById(person.Id);

        if (savedPerson == null)
        {
            return null;
        }
        
        _dbContext.Entry(savedPerson).CurrentValues.SetValues(person);
        _dbContext.SaveChanges();
        
        return savedPerson;
    }

    public string? DeletePerson(int id)
    {
        Person? savedPerson = GetPersonById(id, true);
        
        if (savedPerson == null)
        {
            return null;
        }

        foreach (var shift in savedPerson.Shifts)
        {
            _dbContext.Shifts.Remove(shift);
        }
        
        _dbContext.People.Remove(savedPerson);
        _dbContext.SaveChanges();
        return $"Successfully deleted person with id {id}";
    }
}