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
        
}