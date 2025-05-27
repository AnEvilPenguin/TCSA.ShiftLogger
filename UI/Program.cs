
using Spectre.Console;
using UI.View;

var mainMenu = new MainMenu();

try
{
    mainMenu.Run();
    return 0;
}
catch (Exception ex)
{
    AnsiConsole.MarkupLine("[darkred]Critical:");
    AnsiConsole.WriteException(ex);
    
    return 1;
}
