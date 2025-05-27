using System.ComponentModel.DataAnnotations;
using Spectre.Console;

namespace UI.View;

enum MainMenuOptions
{
    [Display(Name = "Manage Shifts")]
    ManageShifts,
    [Display(Name = "Manage People")]
    ManagePeople,
    Quit
}

public class MainMenu : AbstractMenu
{
    public void Run()
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
                    break;
            }
        }
    }
}