using MinecraftDatapackReloadHelper.API.Rcon;

namespace MinecraftDatapackReloadHelper.Tools
{
    internal class ConnectionTest
    {
        internal static async Task ConnectingTesterAsync()
        {
            var connection = RconConnector.GetRconInst();
            try
            {
                Console.WriteLine(await connection.SendCommandAsync("say Rcon was connected"));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}