using MinecraftDatapackReloadHelper.Libs.Command;
using MinecraftDatapackReloadHelper.System.Control.Setting;
using MinecraftDatapackReloadHelper.Tools;

namespace MinecraftDatapackReloadHelper.System.Control
{
    internal class AppCommandTerminal
    {
        internal static async void Run()
        {
            while (true)
            {
                string? command = string.Empty;
                Console.Write("> ");

                while (string.IsNullOrWhiteSpace(command) && string.IsNullOrEmpty(command))
                    command = Console.ReadLine();

                await CommandSelector.RunCommand(ArgsParser.Parse(command));
            }
        }
    }
}