using MinecraftDatapackReloadHelper.Libs.Files.Directories;
using MinecraftDatapackReloadHelper.Tools;
using MinecraftDatapackReloadHelper.Libs.Rcon;

namespace MinecraftDatapackReloadHelper.Tools
{
    internal class AdvReloader
    {
        internal static async Task ReloadAsync(string source, string copy, bool copyOnly)
        {
            // ex
            bool exceptioned = false;

            //inst
            var connection = RconConnector.GetRconInst();

            //test
            try
            {
                if (!copyOnly)
                    await connection.SendCommandAsync("say Copying Files...");
            }
            catch (Exception ex)
            {
                Display.Message.Error(ex.Message);
                Display.Message.Error(ex.StackTrace);
                exceptioned = true;
            }

            if (!exceptioned)
            {
                //copy
                Display.Message.Warning("Copying datapack folder...");
                var dir = new DirectoryInfo(source);
                Console.ForegroundColor = ConsoleColor.Green;
                DirectoryCopy.Copy(source, Path.Combine(copy, dir.Name), true);
                Console.ForegroundColor = ConsoleColor.White;

                //reload
                if (!copyOnly)
                    Console.WriteLine(await connection.SendCommandAsync("reload"));
            }
        }
    }
}