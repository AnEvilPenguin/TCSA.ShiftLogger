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

public class MainMenu(PersonController people) : AbstractMenu
{
    private PeopleMenu _people = new (people);
    
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
                    break;
                
                case MainMenuOptions.ManagePeople:
                    await _people.Run();
                    break;
            }
        }
    }
}