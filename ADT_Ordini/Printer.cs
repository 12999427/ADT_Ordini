namespace ADT_Ordini;

static class Printer
{
    private static readonly Dictionary<string, ConsoleColor> _colors = new()
    {
        { nameof(TerminaleUtente), ConsoleColor.Cyan },
        { nameof(Chef),            ConsoleColor.Green },
        { nameof(CervelloCentrale),ConsoleColor.Yellow },
        { nameof(Drone),           ConsoleColor.Magenta },
        { "DeliveryStack",         ConsoleColor.Blue },
    };

    public static void Print(string message, string callerClass)
    {
        var color = _colors.TryGetValue(callerClass, out var c) ? c : ConsoleColor.White;
        var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
        var prev = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine($"[{timestamp}] {message}");
        Console.ForegroundColor = prev;
    }
}
