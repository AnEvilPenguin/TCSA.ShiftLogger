﻿using System.ComponentModel.DataAnnotations;
using Spectre.Console;
using UI.Controllers;
using static UI.Util.ViewHelpers;

namespace UI.View;

enum PeopleMenuOptions
{
    [Display(Name = "List People")]
    List,
    [Display(Name = "Add Person")]
    Add,
    [Display(Name = "Update Person")]
    Update,
    [Display(Name = "Delete Person")]
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

public class PeopleMenu (PersonController personController) : AbstractMenu ()
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
        var list = await personController.ListPeople();
        
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
        
        var person = await personController.AddPerson(firstName, lastName);
        
        if (person != null)
            PeopleView.ShowPerson(person);
    }

    private async Task UpdatePerson()
    {
        var people = await personController.ListPeople();
        var person = SelectPerson(people);
        
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
        
        var updated = await personController.UpdatePerson(update);
        
        if (updated != null)
            PeopleView.ShowPerson(updated);
    }

    private async Task DeletePerson()
    {
        var people = await personController.ListPeople();
        var person = SelectPerson(people);
        
        if  (person == null)
            return;

        if (!AnsiConsole.Confirm($"Are you sure you want to [red]delete[/] {person.FirstName} {person.LastName}?"))
            return;
        
        var response = await personController.DeletePerson(person);

        if (response == null)
        {
            AnsiConsole.MarkupLine("[bold red]Could not delete person[/]");
            Pause();
            return;
        }
        
        AnsiConsole.Clear();
        AnsiConsole.WriteLine(response);
        Pause();
    }
}