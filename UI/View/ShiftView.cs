using Spectre.Console;
using UI.Model;
using static UI.Util.ViewHelpers;

namespace UI.View;

public static class ShiftView
{
    public static void ListShifts(Person person, List<Shift> shifts)
    {
        AnsiConsole.Clear();

        WriteHeading(person);

        var table = new Table();
        table.AddColumn(new TableColumn("Start"));
        table.AddColumn(new TableColumn("End"));
        table.AddColumn(new TableColumn("Duration"));

        foreach (var shift in shifts)
            table.AddRow(
                shift.Start.ToString("g"), 
                shift.End.ToString("g"),
                shift.GetDuration());
        
        AnsiConsole.Write(table);

        Pause();
    }

    public static void ShowShift(Person person, Shift shift)
    {
        AnsiConsole.Clear();
        
        WriteHeading(person);
        
        var grid = new Grid()
            .AddColumn()
            .AddColumn();
        
        grid.AddRow("[green]Start[/]", shift.Start.ToString("g"));
        grid.AddRow("[blue]End[/]", shift.End.ToString("g"));
        grid.AddRow("[darkorange]Duration[/]", shift.GetDuration());
        
        AnsiConsole.Write(grid);
        
        Pause();
    }

    private static void WriteHeading(Person person)
    {
        var rule = new Rule($"[darkorange]{person.FirstName} {person.LastName}[/]").LeftJustified();
        AnsiConsole.Write(rule);
    }
}