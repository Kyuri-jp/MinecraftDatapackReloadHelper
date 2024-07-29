namespace MinecraftDatapackReloadHelper.Tools.Display;

internal class Message
{
    internal static void Error(string? message) => Write(message, ConsoleColor.Red);

    internal static void Warning(string? message) => Write(message, ConsoleColor.Yellow);

    private static void Write(string? message, ConsoleColor color)
    {
        ConsoleColor consoleColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = consoleColor;
    }
}