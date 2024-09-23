using CoreRCON;

namespace MinecraftDatapackReloadHelper.Libs.Network.Rcon
{
    internal class RconInterfaces
    {
        internal static async Task<string> SendCommandAsync(string command)
        {
            using RCON rcon = RconConnector.GetRconInst();
            return await rcon.SendCommandAsync(command);
        }
    }
}