using CoreRCON;
using System.Net;

namespace MinecraftDatapackReloadHelper.Libs.Rcon
{
    internal class RconConnector
    {
        internal static RCON GetRconInst()
        {
            RCON connection = new(IPAddress.Parse(GetValues()[0]), ushort.Parse(GetValues()[1]), GetValues()[2]);
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

        private static List<string> GetValues() => [Settings.Rconip, Settings.Rconport.ToString(), Settings.Rconpassword];
    }
}