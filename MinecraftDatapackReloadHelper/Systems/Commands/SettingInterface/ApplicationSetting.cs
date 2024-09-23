using MinecraftDatapackReloadHelper.Libs.Console.Asker;
using MinecraftDatapackReloadHelper.Libs.Files;
using MinecraftDatapackReloadHelper.Libs.Minecraft;
using MinecraftDatapackReloadHelper.Systems.Control;
using System.Net;
using System.Net.Sockets;

namespace MinecraftDatapackReloadHelper.Systems.Commands.SettingInterface
{
    internal class ApplicationSetting
    {
        internal static async Task ChangeRconSettingAsync(bool auto = false)
        {
            string rconIp;
            string rconPort;
            string rconPass;
            if (auto)
            {
                DirectoryInfo copyDirectoryInfo = new(Settings.Copypath);
                string filePath = Path.Combine(copyDirectoryInfo.Parent!.Parent!.FullName, "server.properties");

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

                Dictionary<string, string> propertyData = ServerProperties.Parse(filePath);

                rconIp = Getv4Adress();
                rconPort = propertyData["rcon.port"];
                rconPass = propertyData["rcon.password"];
            }
            else
            {
                rconIp = Asker.Ask("Please enter rcon ipadress.");
                if (rconIp == "localhost")
                {
                    Tools.Display.Message.Warning("Inputed localhost.\nset this computer's private ip adress.");

                    rconIp = Getv4Adress();
                }

                rconPort = Asker.Ask("Please enter rcon port.");
                rconPass = Asker.Ask("Please enter rcon password");
            }

            if (rconIp != ":skip")
                Settings.Rconip = rconIp;

            if (rconPort != ":skip")
                Settings.Rconport = ushort.Parse(rconPort);

            if (rconPass != ":skip")
                Settings.Rconpassword = rconPass;

            Settings.Default.Save();

            await ConnectionTest.ConnectingTesterAsync();
        }

        private static string Getv4Adress()
        {
            string? v4 = (from ip in Dns.GetHostAddresses(Dns.GetHostName()) where ip.AddressFamily.Equals(AddressFamily.InterNetwork) select ip.ToString()).FirstOrDefault();
            ArgumentException.ThrowIfNullOrEmpty(v4);
            return v4;
        }
    }
}