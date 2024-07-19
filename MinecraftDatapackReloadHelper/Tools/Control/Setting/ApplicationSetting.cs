using System.Net;
using System.Net.Sockets;
using MinecraftDatapackReloadHelper.Tools.Minecraft;

namespace MinecraftDatapackReloadHelper.Tools.Control.Setting
{
    internal class ApplicationSetting
    {
        internal static async Task ChangeRconSettingAsync()
        {
            string? rconIP = Asker("Please enter rcon ipadress.");
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

            string? rconPort = Asker("Please enter rcon port.");
            string? rconPass = Asker("Please enter rcon password", true);

            if (rconIP != ":skip")
                Settings.Rcon_IP = rconIP;

            if (rconPort != ":skip")
                Settings.Rcon_Port = ushort.Parse(rconPort);

            if (rconPass != ":skip")
                Settings.Rcon_Password = rconPass;

            Settings.Default.Save();

            await ConnectionTest.ConnectingTesterAsync();
        }

        private static string Asker(string message, bool mayNull = false)
        {
            string? reader = string.Empty;

            while (reader == string.Empty)
            {
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
                reader = Console.ReadLine();

                if (mayNull)
                    break;

                if (reader == null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Please enter any strings.");
                    reader = string.Empty;
                    continue;
                }
            }

            return reader;
        }

        internal static async Task AutoChangeRconSettingAsync()
        {
            ServerProperties.Parse(Settings.Client_Copy);

            await ConnectionTest.ConnectingTesterAsync();
        }
    }
}