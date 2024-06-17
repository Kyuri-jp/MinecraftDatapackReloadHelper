using CoreRCON;
using System.Net;

namespace MinecraftDatapackReloadHelper.API.Rcon
{
    internal class RconConnector
    {
        internal static RCON GetRconInst()
        {
            List<string> rconInfo = GetValues();
            try
            {
                var connection = new RCON(IPAddress.Parse(rconInfo[0]), ushort.Parse(rconInfo[1]), rconInfo[2]);
                connection.ConnectAsync();
                return connection;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ForegroundColor = ConsoleColor.White;
            }

#pragma warning disable CS8603 // Null 参照戻り値である可能性があります。
            return default;
#pragma warning restore CS8603 // Null 参照戻り値である可能性があります。
        }

        private static List<string> GetValues()
        {
            try
            {
                List<string> values = [Settings.Rcon_IP, Settings.Rcon_Port.ToString(), Settings.Rcon_Password];
                return values;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ForegroundColor = ConsoleColor.White;
            }

#pragma warning disable CS8603 // Null 参照戻り値である可能性があります。
            return default;
#pragma warning restore CS8603 // Null 参照戻り値である可能性があります。
        }
    }
}