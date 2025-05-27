using System.ComponentModel.DataAnnotations;
using System.IO.Pipes;
using Spectre.Console;
using UI.Controllers;
using UI.Model;
using UI.Util;

namespace UI.View;

enum PeopleMenuOptions
{
    List,
    Add,
    Update,
    Delete,
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
                
                case PeopleMenuOptions.Delete:
                    await DeletePerson();
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
        
        if  (person == null)
            return;
        
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

    private async Task DeletePerson()
    {
        var person = await SelectPerson();
        
        if  (person == null)
            return;

        if (!AnsiConsole.Confirm($"Are you sure you want to [red]delete[/] {person.FirstName} {person.LastName}?"))
            return;
        
        var response = await people.DeletePerson(person);
        
        AnsiConsole.Clear();
        AnsiConsole.WriteLine(response);
        Pause();
    }

    private async Task<Person?> SelectPerson()
    {
        var list = await people.ListPeople();

        if (list.Count == 0)
        {
            NoPeople();
            return null;
        }

        var choice = AnsiConsole.Prompt(new SelectionPrompt<Person>()
            .Title("Select person")
            .AddChoices(list)
            .UseConverter(p => $"{p.FirstName} {p.LastName}"));
        
        return choice;
    }

    private void NoPeople()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[red]No people available.[/] Please add a person first.");
        Pause();
    }
}