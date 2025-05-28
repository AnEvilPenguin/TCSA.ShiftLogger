using Spectre.Console;
using UI.Model;

namespace UI.Util;

public static class ViewHelpers
{
    public static void Pause()
    {
        AnsiConsole.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    
    public static Person? SelectPerson(List<Person> list)
    {
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
    
    private static void NoPeople()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[red]No people available.[/] Please add a person first.");
        Pause();
    }
}