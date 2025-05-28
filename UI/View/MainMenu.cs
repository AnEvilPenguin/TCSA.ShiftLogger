using System.ComponentModel.DataAnnotations;
using Spectre.Console;
using UI.Controllers;

namespace UI.View;

enum MainMenuOptions
{
    [Display(Name = "Manage Shifts")]
    ManageShifts,
    [Display(Name = "Manage People")]
    ManagePeople,
    Quit
}

public class MainMenu(PersonController personController, ShiftController shiftController) : AbstractMenu()
{
    private readonly PeopleMenu _people = new (personController);
    private readonly ShiftMenu _shifts = new  (personController, shiftController);
    
    public async Task Run()
    {
        MainMenuOptions? choice = null;

        while (choice != MainMenuOptions.Quit)
        {
            AnsiConsole.Clear();

            choice = Prompt<MainMenuOptions>();

            switch (choice)
            {
                case MainMenuOptions.ManageShifts:
                    await _shifts.Run();
                    break;
                
                case MainMenuOptions.ManagePeople:
                    await _people.Run();
                    break;
            }
        }
    }
}