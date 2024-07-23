using MinecraftDatapackReloadHelper.Libs.Command;
using MinecraftDatapackReloadHelper.Systems.Commands;
using MinecraftDatapackReloadHelper.Systems.Control.Setting;
using MinecraftDatapackReloadHelper.Tools;

namespace MinecraftDatapackReloadHelper.Systems.Control
{
    internal class AppCommandTerminal
    {
        internal static async Task Run()
        {
            while (true)
            {
                string? command = string.Empty;

                while (string.IsNullOrWhiteSpace(command) || string.IsNullOrEmpty(command))
                {
                    Console.Write("> ");
                    command = Console.ReadLine();
                }

                await CommandSelector.RunCommand(ArgsParser.Parse(command));
            }
        }
    }
}