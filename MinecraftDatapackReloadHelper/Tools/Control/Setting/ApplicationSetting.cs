﻿using MinecraftDatapackReloadHelper.Tools.Minecraft;
using System.Net;
using System.Net.Sockets;

namespace MinecraftDatapackReloadHelper.Tools.Control.Setting
{
    internal class ApplicationSetting
    {
        internal static async Task ChangeRconSettingAsync()
        {
            string? rconIP = Asker("Please enter rcon ipadress.");
            if (rconIP == "localhost")
            {
                Display.Console.Warning("Inputed localhost.\nset this computer's private ip adress.");

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
                    Display.Console.Warning("Please enter any strings.");
                    reader = string.Empty;
                    continue;
                }
            }

#pragma warning disable CS8603 // Null 参照戻り値である可能性があります。
            return reader;
#pragma warning restore CS8603 // Null 参照戻り値である可能性があります。
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
                Display.Console.Warning("Copy path is null or empty.");

                while (true)
                {
                    string copy = Asker("Please enter copy path.");
                    if (!Directory.Exists(copy))
                    {
                        Display.Console.Warning($"{copy} is not exists.");
                        continue;
                    }
                    RecursiveFileSearcher recursiveFileSearcher = new();

                    if (!RecursiveFileSearcher.RecursiveFileExists(copy, "level.dat"))
                    {
                        Display.Console.Warning($"Not found level file in {copy}'s parents");
                        continue;
                    }
                    if (!RecursiveFileSearcher.RecursiveFileExists(copy, "server.properties"))
                    {
                        Display.Console.Warning($"Not found server.properties in {copy}'s parents.\nMaybe this directory is not server.");
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