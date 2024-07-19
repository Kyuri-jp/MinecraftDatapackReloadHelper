using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using CoreRCON;
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

                rconIP = Getv4Adress();
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
            ArgumentException.ThrowIfNullOrWhiteSpace(message);
            ArgumentException.ThrowIfNullOrEmpty(message);

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

        private static string Getv4Adress()
        {
            string? v4 = null;
            foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (ip.AddressFamily.Equals(AddressFamily.InterNetwork))
                {
                    v4 = ip.ToString();
                    break;
                }
            }
            ArgumentException.ThrowIfNullOrEmpty(v4);
            return v4;
        }

        internal static async Task AutoChangeRconSettingAsync()
        {
            DirectoryInfo copyDirectoryInfo = new(Settings.Client_Copy); ;

#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
            string filePath = Path.Combine(copyDirectoryInfo.Parent.Parent.FullName, "server.properties");
#pragma warning restore CS8602 // null 参照の可能性があるものの逆参照です。

            if (!File.Exists(filePath))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Copy path is null or empty.");
                Console.ForegroundColor = ConsoleColor.White;

                while (true)
                {
                    string copy = Asker("Please enter copy path.");
                    if (!Directory.Exists(copy))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"{copy} is not exists.");
                        continue;
                    }
                    RecursiveFileSearcher recursiveFileSearcher = new();

                    if (!recursiveFileSearcher.RecursiveFileExists(copy, "level.dat"))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Not found level file in {copy}'s parents");
                        continue;
                    }
                    if (!recursiveFileSearcher.RecursiveFileExists(copy, "server.properties"))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Not found server.properties in {copy}'s parents.\nMaybe this directory is not server.");
                        continue;
                    }

                    break;
                }
            }

            Dictionary<string, string> PropertyData = ServerProperties.Parse(filePath);

            Settings.Rcon_IP = Getv4Adress();
            Settings.Rcon_Port = ushort.Parse(PropertyData["rcon.port"]);
            Settings.Rcon_Password = PropertyData["rcon.password"];

            await ConnectionTest.ConnectingTesterAsync();
        }
    }
}