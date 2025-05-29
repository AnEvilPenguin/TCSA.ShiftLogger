using System.ComponentModel.DataAnnotations;
using Spectre.Console;
using UI.Controllers;
using UI.Model;
using static UI.Util.ViewHelpers;
using static UI.View.ShiftView;
using ValidationResult = Spectre.Console.ValidationResult;

namespace UI.View;

enum ShiftMenuOptions
{
    [Display(Name = "Select Person")]
    SelectPerson,
    [Display(Name = "List Existing Shifts")]
    ListShifts,
    [Display(Name = "Create Shift")]
    AddShift,
    [Display(Name = "Delete Shift")]
    DeleteShift,
    Back
}

public class ShiftMenu(PersonController personController, ShiftController shiftController) : AbstractMenu
{
    private Person? _selectedPerson;

    public async Task Run()
    {
        ShiftMenuOptions? choice = null;

        while (choice != ShiftMenuOptions.Back)
        {
            AnsiConsole.Clear();

            choice = SelectChoice();

            switch (choice)
            {
                case ShiftMenuOptions.SelectPerson:
                    var people = await personController.ListPeople();
                    _selectedPerson = SelectPerson(people);
                    break;
                
                case ShiftMenuOptions.ListShifts:
                    if (_selectedPerson == null)
                        break;
                    
                    var shifts = await shiftController.GetShifts(_selectedPerson.Id);
                    ListShifts(_selectedPerson, shifts);
                    break;
                
                case ShiftMenuOptions.AddShift:
                    await CreateShift();
                    break;
                
                case ShiftMenuOptions.DeleteShift:
                    await DeleteShift();
                    break;
            }
        }
    }

    private ShiftMenuOptions SelectChoice()
    {
        if (_selectedPerson == null)
        {
            return AnsiConsole.Prompt(new SelectionPrompt<ShiftMenuOptions>()
                .Title("What would you like to do?")
                .AddChoices([ShiftMenuOptions.SelectPerson, ShiftMenuOptions.Back])
                .UseConverter(GetEnumDisplayValue));
        }
        
        return Prompt<ShiftMenuOptions>();
    }

    private async Task CreateShift()
    {
        if (_selectedPerson == null)
            return;

        WriteDivider("Start");
        var start = AnsiConsole.Prompt(
            new TextPrompt<DateTime>("When did the shift start?")
                .Validate(date =>
                {
                    if (date < DateTime.Now)
                        return ValidationResult.Success();

                    return ValidationResult.Error("[red]Shift start cannot be in the future[/]");
                }));
        
        WriteDivider("End");
        var end = AnsiConsole.Prompt(
            new TextPrompt<DateTime>("When did the shift end?")
                .Validate(date =>
                {
                    if (date > DateTime.Now)
                        return ValidationResult.Error("[red]Shift end cannot be in the future[/]");
                    
                    if (date < start)
                        return ValidationResult.Error("[red]Shift end cannot be before start[/]");

                    // Sweatshop conditions are fine.
                    return ValidationResult.Success();
                }));
        
        if (!AnsiConsole.Confirm("Are you sure you want to create a new shift?"))
            return;

        var shift = await shiftController.AddShift(_selectedPerson.Id, start, end);

        if (shift == null)
        {
            AnsiConsole.Markup("[red]Error creating new shift[/]");
            return;
        }
        
        ShowShift(_selectedPerson, shift);
    }
     
    private async Task DeleteShift()
    {
        if (_selectedPerson == null)
            return;
        
        var shifts = await shiftController.GetShifts(_selectedPerson.Id);
        
        if  (shifts.Count == 0)
        {
            AnsiConsole.Markup("[red]Nothing to remove[/]");
            return;
        }

        var shift = AnsiConsole.Prompt(
            new SelectionPrompt<Shift>()
                .Title("Which shift would you like to delete?")
                .AddChoices(shifts)
                .UseConverter(s => $"start: {s.Start:g}, end: {s.End:g}"));
        
        if (!AnsiConsole.Confirm("Are you sure you want to delete this shift?"))
            return;
        
        var response = await shiftController.RemoveShift(shift);
        
        if (response == null)
        {
            AnsiConsole.Markup("[red]Failed to remove shift.[/] It may have already been removed.");
            return;
        }

        AnsiConsole.WriteLine(response);
        
        Pause();
    }
}