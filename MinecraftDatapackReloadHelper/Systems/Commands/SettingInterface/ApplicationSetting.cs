using MinecraftDatapackReloadHelper.Libs.Console.Asker;
using MinecraftDatapackReloadHelper.Libs.Files;
using MinecraftDatapackReloadHelper.Libs.Minecraft;
using MinecraftDatapackReloadHelper.Tools;
using System.Net;
using System.Net.Sockets;

namespace MinecraftDatapackReloadHelper.Systems.Commands.SettingInterface
{
    internal class ApplicationSetting
    {
        internal static async Task ChangeRconSettingAsync(bool auto = false)
        {
            string rconIP;
            string rconPort;
            string rconPass;
            if (auto)
            {
                DirectoryInfo copyDirectoryInfo = new(Settings.Client_Copy);
                string filePath = Path.Combine(copyDirectoryInfo!.Parent!.Parent!.FullName, "server.properties");

                if (!File.Exists(filePath))
                {
                    Tools.Display.Message.Warning("Copy path is null or empty.");

                    while (true)
                    {
                        string copy = Asker.PathAsk("Please enter copy path.", true);
                        if (!RecursiveSearch.FileExists(copy, "level.dat"))
                        {
                            Tools.Display.Message.Warning($"Not found level file in {copy}'s parents");
                            continue;
                        }
                        if (!RecursiveSearch.FileExists(copy, "server.properties"))
                        {
                            Tools.Display.Message.Warning($"Not found server.properties in {copy}'s parents.\nMaybe this directory is not server.");
                            continue;
                        }

                        break;
                    }
                }

                Dictionary<string, string> PropertyData = ServerProperties.Parse(filePath);

                rconIP = Getv4Adress();
                rconPort = PropertyData["rcon.port"];
                rconPass = PropertyData["rcon.password"];
            }
            else
            {
                rconIP = Asker.Ask("Please enter rcon ipadress.");
                if (rconIP == "localhost")
                {
                    Tools.Display.Message.Warning("Inputed localhost.\nset this computer's private ip adress.");

                    rconIP = Getv4Adress();
                }

                rconPort = Asker.Ask("Please enter rcon port.");
                rconPass = Asker.Ask("Please enter rcon password");
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
    }
}