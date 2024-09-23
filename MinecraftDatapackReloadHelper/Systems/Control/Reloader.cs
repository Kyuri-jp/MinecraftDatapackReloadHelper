using MinecraftDatapackReloadHelper.Libs.Console;
using MinecraftDatapackReloadHelper.Libs.Files.Directories;
using MinecraftDatapackReloadHelper.Libs.Rcon;

namespace MinecraftDatapackReloadHelper.Systems.Control
{
    internal class Reloader
    {
        internal static async Task ReloadAsync(string source, string copy, bool copyOnly = false)
        {
            // ex
            bool exceptioned = false;

            //test
            try
            {
                if (!copyOnly)
                    await RconInterfaces.SendCommandAsync("say Copying Files...");
            }
            catch (Exception ex)
            {
                Message.Error(ex.Message);
                Message.Error(ex.StackTrace);
                exceptioned = true;
            }

            if (!exceptioned)
            {
                //copy
                Message.Warning("Copying datapack folder...");
                var dir = new DirectoryInfo(source);
                Console.ForegroundColor = ConsoleColor.Green;
                DirectoryCopy.Copy(source, Path.Combine(copy, dir.Name), true);
                Console.ForegroundColor = ConsoleColor.White;

                //reload
                if (!copyOnly)
                    Console.WriteLine(await RconInterfaces.SendCommandAsync("reload"));
            }
        }
    }
}