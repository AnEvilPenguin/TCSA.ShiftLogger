using System.ComponentModel.DataAnnotations;
using Spectre.Console;
using UI.Util;

namespace UI.View;

public abstract class AbstractMenu
{
    protected static T Prompt<T>(string title = "What would you like to do?") where T : struct, Enum =>
        AnsiConsole.Prompt(new SelectionPrompt<T>()
            .Title(title)
            .AddChoices(Enum.GetValues<T>())
            .UseConverter(GetEnumDisplayValue));
    
    private static string GetEnumDisplayValue<T>(T enumValue) where T : Enum
    {
        var displayAttribute = enumValue
            .GetType()
            .GetField(enumValue.ToString())
            ?.GetCustomAttributes(typeof(DisplayAttribute), false)
            .FirstOrDefault() as DisplayAttribute;

        return displayAttribute?.Name ?? enumValue.ToString();
    }
    
    protected static void Pause() =>
        ViewHelpers.Pause();
}