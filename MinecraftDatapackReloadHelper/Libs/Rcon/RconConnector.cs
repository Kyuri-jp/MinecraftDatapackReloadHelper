using CoreRCON;
using System.Net;
using UtilForMinecraftLibrary.Server.Rcon;

namespace MinecraftDatapackReloadHelper.Libs.Rcon
{
    internal class RconConnector
    {
        internal static RCON AutoGetRconInstans() => Connect.GetRconInstans(GetValues()[0], GetValues()[1], GetValues()[2]);

        private static List<dynamic> GetValues() => [IPAddress.Parse(Settings.Rcon_IP), Settings.Rcon_Port, Settings.Rcon_Password];
    }
}