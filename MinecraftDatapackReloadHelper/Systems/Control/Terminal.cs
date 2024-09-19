using MinecraftDatapackReloadHelper.Libs.Rcon;

namespace MinecraftDatapackReloadHelper.Systems.Control
{
    internal class Terminal
    {
        internal static async Task RunAsync()
        {
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
                        Tools.Display.Message.Warning("Please enter any command.\n");
                        command = string.Empty;
                        continue;
                    }
                }

                if (command == "exit")
                    break;

                try
                {
                    Console.WriteLine(await RconInterfaces.SendCommandAsync(command));
                }
                catch (Exception ex)
                {
                    Tools.Display.Message.Error(ex.Message);
                    Tools.Display.Message.Error(ex.StackTrace);
                    continue;
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = consoleColor;
        }
    }
}