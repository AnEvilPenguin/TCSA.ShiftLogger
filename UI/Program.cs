using Spectre.Console;
using UI.Controllers;
using UI.View;

var baseUrl = "http://localhost:5003/api/";
var peopleApi = new PersonController(baseUrl);
var shiftApi = new ShiftController(baseUrl);

var mainMenu = new MainMenu(peopleApi);

try
{
    await mainMenu.Run();
    return 0;
}
catch (Exception ex)
{
    AnsiConsole.MarkupLine("[darkred]Critical:[/]");
    AnsiConsole.WriteException(ex);
    
    return 1;
}
