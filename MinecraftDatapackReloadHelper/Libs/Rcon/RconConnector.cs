using CoreRCON;
using System.Net;

namespace MinecraftDatapackReloadHelper.Libs.Rcon
{
    internal class RconConnector
    {
        internal static RCON GetRconInst()
        {
            using var connection = new RCON(IPAddress.Parse(GetValues()[0]), ushort.Parse(GetValues()[1]), GetValues()[2]);
            try
            {
                connection.ConnectAsync();
            }
            catch (Exception ex)
            {
                Tools.Display.Message.Error(ex.Message);
                Tools.Display.Message.Error(ex.StackTrace);
            }
            return connection;
        }

        private static List<string> GetValues() => [Settings.Rcon_IP, Settings.Rcon_Port.ToString(), Settings.Rcon_Password];
    }
}