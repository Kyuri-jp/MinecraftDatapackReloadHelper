namespace MinecraftDatapackReloadHelper.Libs.Console;

internal class Message
{
    internal static void Error(string? message) => Write(message, ConsoleColor.Red);

    internal static void Warning(string? message) => Write(message, ConsoleColor.Yellow);

    private static void Write(string? message, ConsoleColor color)
    {
        ConsoleColor consoleColor = System.Console.ForegroundColor;
        System.Console.ForegroundColor = color;
        System.Console.WriteLine(message);
        System.Console.ForegroundColor = consoleColor;
    }
}