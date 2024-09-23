using MinecraftDatapackReloadHelper.Libs.Console;
using MinecraftDatapackReloadHelper.Libs.Rcon;

namespace MinecraftDatapackReloadHelper.Systems.Control
{
    internal class ConnectionTest
    {
        internal static async Task ConnectingTesterAsync()
        {
            try
            {
                Console.WriteLine(await RconInterfaces.SendCommandAsync("say [MDRH] Rcon was connected"));
            }
            catch (Exception ex)
            {
                Message.Error(ex.Message);
                Message.Error(ex.StackTrace);
            }
        }
    }
}