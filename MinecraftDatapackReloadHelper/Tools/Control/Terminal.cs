using CoreRCON;
using MinecraftDatapackReloadHelper.API.Rcon;

namespace MinecraftDatapackReloadHelper.Tools.Control
{
    internal class Terminal
    {
        internal static async Task Run()
        {
            var connection = RconConnector.GetRconInst();

            ConsoleColor consoleColor = Console.BackgroundColor;

            Console.BackgroundColor = ConsoleColor.DarkGray;

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                string command = string.Empty;
                Console.Write("> ");
                while (command == string.Empty)
                {
                    command = Console.ReadLine();
                    if (command == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Please enter any command.\n");
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    continue;
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = consoleColor;
        }
    }
}