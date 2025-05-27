using System.ComponentModel.DataAnnotations;
using Spectre.Console;
using UI.Controllers;
using UI.Model;

namespace UI.View;

enum PeopleMenuOptions
{
    List,
    Add,
    Update,
    Back
}

enum UpdateOptions
{
    [Display(Name = "First Name")]
    First,
    [Display(Name = "Last Name")]
    Last
}

public class PeopleMenu (PersonController people) : AbstractMenu
{
    public async Task Run()
    {
        PeopleMenuOptions? choice = null;

        while (choice != PeopleMenuOptions.Back)
        {
            AnsiConsole.Clear();
            
            choice = Prompt<PeopleMenuOptions>();

            switch (choice)
            {
                case PeopleMenuOptions.List:
                    await ListPeople();
                    break;
                
                case PeopleMenuOptions.Add:
                    await AddPerson();
                    break;
                
                case PeopleMenuOptions.Update:
                    await UpdatePerson();
                    break;
            }
        }
    }
    
    private async Task ListPeople()
    {
        var list = await people.ListPeople();
        
        PeopleView.ListPeople(list);
    }

    private async Task AddPerson()
    {
        var firstName = AnsiConsole.Ask<string>("What is the first name?");
        var lastName = AnsiConsole.Ask<string>("What is the last name?");
        
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"You entered: [bold]{firstName} {lastName}[/]");

        if (!AnsiConsole.Confirm("Is this correct?"))
            return;
        
        var person = await people.AddPerson(firstName, lastName);
        
        if (person != null)
            PeopleView.ShowPerson(person);
    }

    private async Task UpdatePerson()
    {
        var person = await SelectPerson();
        
        PeopleView.ShowPerson(person, false);
        
        var choice = Prompt<UpdateOptions>("What would you like to update");

        var current = choice switch
        {
            UpdateOptions.First => person.FirstName,
            _ => person.LastName
        };

        var change = AnsiConsole.Ask<string>($"What would you like to update [green]{current}[/] to?");
        
        if (!AnsiConsole.Confirm($"Change [green]{current}[/] to [yellow]{change}[/]?"))
            return;

        var update = choice switch
        {
            UpdateOptions.First => person with { FirstName = change },
            _ => person with { LastName = change }
        };
        
        var updated = await people.UpdatePerson(update);
        
        if (updated != null)
            PeopleView.ShowPerson(updated);
    }

    private async Task<Person> SelectPerson()
    {
        var list = await people.ListPeople();

        var choice = AnsiConsole.Prompt(new SelectionPrompt<Person>()
            .Title("Select person")
            .AddChoices(list)
            .UseConverter(p => $"{p.FirstName} {p.LastName}"));
        
        return choice;
    }
}