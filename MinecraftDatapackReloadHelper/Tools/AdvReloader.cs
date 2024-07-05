using MinecraftDatapackReloadHelper.API.Rcon;
using MinecraftDatapackReloadHelper.Tools;

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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ForegroundColor = ConsoleColor.White;
                exceptioned = true;
            }

            if (!exceptioned)
            {
                //copy
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Copying...");
                var dir = new DirectoryInfo(source);
                DirectoryCopy.Copy(source, Path.Combine(copy, dir.Name), true);
                Console.ForegroundColor = ConsoleColor.White;

                //reload
                if (!copyOnly)
                    Console.WriteLine(await connection.SendCommandAsync("reload"));
            }
        }
    }
}