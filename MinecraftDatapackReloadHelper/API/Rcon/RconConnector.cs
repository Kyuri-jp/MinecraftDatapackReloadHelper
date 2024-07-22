using CoreRCON;
using System.Net;

namespace MinecraftDatapackReloadHelper.API.Rcon
{
    internal class RconConnector
    {
        internal static RCON GetRconInst()
        {
            var connection = new RCON(IPAddress.Parse(GetValues()[0]), ushort.Parse(GetValues()[1]), GetValues()[2]);
            try
            {
                connection.ConnectAsync();
            }
            catch (Exception ex)
            {
                Tools.Display.Console.Error(ex.Message);
                Tools.Display.Console.Error(ex.StackTrace);
            }

            return connection;
        }

        private static List<string> GetValues() => [Settings.Rcon_IP, Settings.Rcon_Port.ToString(), Settings.Rcon_Password];
    }
}