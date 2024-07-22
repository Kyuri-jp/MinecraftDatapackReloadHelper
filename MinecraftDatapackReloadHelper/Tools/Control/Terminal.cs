using MinecraftDatapackReloadHelper.Libs.Rcon;

namespace MinecraftDatapackReloadHelper.Tools.Control
{
    internal class Terminal
    {
        internal static async Task RunAsync()
        {
            var connection = RconConnector.GetRconInst();

            ConsoleColor consoleColor = Console.BackgroundColor;

            Console.BackgroundColor = ConsoleColor.DarkGray;

            Console.WriteLine("Type exit to exit the terminal.");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                string? command = string.Empty;
                Console.Write("> ");
                while (command == string.Empty)
                {
                    command = Console.ReadLine();
                    if (command == null)
                    {
                        Display.Console.Warning("Please enter any command.\n");
                        command = string.Empty;
                        continue;
                    }
                }

                if (command == "exit")
                    break;

                try
                {
                    Console.WriteLine(await connection.SendCommandAsync(command));
                }
                catch (Exception ex)
                {
                    Display.Console.Error(ex.Message);
                    Display.Console.Error(ex.StackTrace);
                    continue;
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = consoleColor;
        }
    }
}