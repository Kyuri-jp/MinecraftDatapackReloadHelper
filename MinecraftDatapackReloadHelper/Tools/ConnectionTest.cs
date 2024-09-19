using MinecraftDatapackReloadHelper.Libs.Rcon;

namespace MinecraftDatapackReloadHelper.Tools
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
                Display.Message.Error(ex.Message);
                Display.Message.Error(ex.StackTrace);
            }
        }
    }
}