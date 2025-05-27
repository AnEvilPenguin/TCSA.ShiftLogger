using Spectre.Console;

namespace UI.Util;

public static class ViewHelpers
{
    public static void Pause()
    {
        AnsiConsole.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}