﻿using MinecraftDatapackReloadHelper.Libs.Console;
using MinecraftDatapackReloadHelper.Libs.Network.Rcon;

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
                    if (command != null) continue;
                    Message.Warning("Please enter any command.\n");
                    command = string.Empty;
                }

                if (command == "exit")
                    break;

                try
                {
                    Console.WriteLine(await RconInterfaces.SendCommandAsync(command));
                }
                catch (Exception ex)
                {
                    Message.Error(ex.Message);
                    Message.Error(ex.StackTrace);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = consoleColor;
        }
    }
}