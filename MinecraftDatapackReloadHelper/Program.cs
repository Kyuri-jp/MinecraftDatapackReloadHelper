using MinecraftDatapackReloadHelper.Tools.Control;

namespace MinecraftDatapackReloadHelper
{
    internal class Programs
    {
        private static readonly string version = "v0.2.0-Alpha";

        private static readonly string welcome =
                    "====================\n" +
                    "Hello!\n" +
                    "This is Minecraft Datapack Reload Helper.\n" +
                    $"Version {version}\n" +
                    "This app is released by MIT License.\n" +
                    "Copyright (c) 2024 Kyuri\n" +
                    "\nUsed Libraries:\n" +
                    "CoreRCON v5.4.1 / MIT License Copyright (c) 2017 Scott Kaye\n" +
                    "System.Configuration.ConfigurationManager v8.0.0 / MIT License Copyright (c) .NET Foundation and Contributors\n" +
                    "====================\n";

        private static async Task Main()
        {
            //message
            Console.WriteLine(welcome);

            Settings.Default.Save();

            //run terminal
            await AppCommandTerminal.Run();
        }

        internal static string GetAppVersion() => version;

        internal static string GetWelcomeMessage() => welcome;
    }
}