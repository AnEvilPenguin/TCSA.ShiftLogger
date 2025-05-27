using Spectre.Console;
using UI.Model;
using static UI.Util.ViewHelpers;

namespace UI.View;

public static class PeopleView
{
    public static void ListPeople(List<Person> people, bool pause = true)
    {
        AnsiConsole.Clear();
        
        var table = new Table();
        
        table.AddColumn(new TableColumn("First name"));
        table.AddColumn(new TableColumn("Last name"));

        foreach (var person in people)
            table.AddRow(person.FirstName, person.LastName);
        
        AnsiConsole.Write(table);
     
        if (pause)
            Pause();
    }

    public static void ShowPerson(Person person, bool pause = true) =>
        ListPeople([person], pause);
}