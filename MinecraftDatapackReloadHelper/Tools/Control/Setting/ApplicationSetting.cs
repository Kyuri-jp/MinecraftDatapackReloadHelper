using System.Net;
using System.Net.Sockets;

namespace MinecraftDatapackReloadHelper.Tools.Control.Setting
{
    internal class ApplicationSetting
    {
        internal static async Task ChangeRconSettingAsync()
        {
            string? rconIP = string.Empty;

            while (rconIP == string.Empty)
            {
                Console.WriteLine("Please enter rcon ip adress.");
                Console.ForegroundColor = ConsoleColor.White;
                rconIP = Console.ReadLine();
                if (rconIP == null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Null.");
                    rconIP = string.Empty;
                    continue;
                }
            }
            if (rconIP == "localhost")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Inputed localhost.\nset this computer's private ip adress.");
                Console.ForegroundColor = ConsoleColor.White;
                foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    if (ip.AddressFamily.Equals(AddressFamily.InterNetwork))
                    {
                        rconIP = ip.ToString();
                        break;
                    }
                }
            }

            string? rconPort = string.Empty;

            while (rconPort == string.Empty)
            {
                Console.WriteLine("Please enter rcon port.");
                Console.ForegroundColor = ConsoleColor.White;
                rconPort = Console.ReadLine();
                if (rconPort == null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Null.");
                    rconPort = string.Empty;
                    continue;
                }
            }
            string? rconPass = string.Empty;

            while (rconPass == string.Empty)
            {
                Console.WriteLine("Please enter rcon password.");
                Console.ForegroundColor = ConsoleColor.White;
                rconPass = Console.ReadLine();
                if (rconPass == null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Null.");
                    rconPass = string.Empty;
                    continue;
                }
            }

            if (rconIP != ":skip")
                Settings.Rcon_IP = rconIP;

            if (rconPort != ":skip")
                Settings.Rcon_Port = ushort.Parse(rconPort);

            if (rconPass != ":skip")
                Settings.Rcon_Password = rconPass;

            Settings.Default.Save();

            await ConnectionTest.ConnectingTesterAsync();
        }
    }
}