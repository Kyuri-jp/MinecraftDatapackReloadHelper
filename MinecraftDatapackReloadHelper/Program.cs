using CoreRCON;
using MinecraftDatapackReloadHelper;
using MinecraftDatapackReloadHelper.API.Rcon;
using MinecraftDatapackReloadHelper.Tools;
using MinecraftDatapackReloadHelper.Tools.Control;
using System.Net;
using System.Net.Sockets;

namespace Programs
{
    internal class Programs
    {
        private static async Task Main()
        {
            //message
            Console.WriteLine("====================\n" +
                "Hello!\n" +
                "This is Minecraft Datapack Reload Helper.\n" +
                "This app is released by MIT License.\n" +
                "Copyright (c) 2024 Kyuri\n" +
                "Used Libraries:" +
                "CoreRCON v5.4.1 MIT License Copyright (c) 2017 Scott Kaye\n" +
                "====================\n");

            Settings.Default.Save();

            string command = string.Empty;

            while (true)
            {
                command = string.Empty;

                Console.Write("> ");

                while (command == string.Empty)
                {
                    command = Console.ReadLine();
                }

                switch (command)
                {
                    case "app-setting":
                        ChangeRconSetting();
                        break;

                    case "path-setting":
                        ChangePathSetting();
                        break;

                    case "connection-test":
                        ConnectingTester();
                        break;

                    case "reload":
                        AdvReloader.Reload(Settings.Client_Source, Settings.Client_Copy);
                        break;

                    case "terminal":
                        await Terminal.Run();
                        break;

                    case "show-setting":
                        Console.WriteLine($"Ip: {Settings.Rcon_IP}\n" +
                            $"Port : {Settings.Rcon_Port}\n" +
                            $"Password : {Settings.Rcon_Password}\n" +
                            $"Source : {Settings.Client_Source}\n" +
                            $"Copy : {Settings.Client_Copy}\n");
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{command} is an invalid command.");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
        }

        private static async void ConnectingTester()
        {
            var connection = RconConnector.GetRconInst();
            Console.WriteLine(await connection.SendCommandAsync("say Rcon was connected"));
        }

        private static void ChangeRconSetting()
        {
            string rconIP = string.Empty;

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

            string rconPort = string.Empty;

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
            string rconPass = string.Empty;

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

            Settings.Rcon_IP = rconIP;
            Settings.Rcon_Port = ushort.Parse(rconPort);
            Settings.Rcon_Password = rconPass;

            Settings.Default.Save();

            ConnectingTester();
        }

        private static void ChangePathSetting()
        {
            string source = string.Empty;

            while (source == string.Empty)
            {
                Console.WriteLine("Please enter source directory path.");
                Console.ForegroundColor = ConsoleColor.White;
                source = Console.ReadLine();
                if (source == null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Null.");
                    source = string.Empty;
                    continue;
                }
                if (!Directory.Exists(source))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{source} is not exists.");
                    source = string.Empty;
                    continue;
                }
            }

            string copy = string.Empty;

            while (copy == string.Empty)
            {
                Console.WriteLine("Please enter copy directory path.");
                Console.ForegroundColor = ConsoleColor.White;
                copy = Console.ReadLine();
                if (copy == null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Null.");
                    copy = string.Empty;
                    continue;
                }
                if (!Directory.Exists(copy))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{copy} is not exists.");
                    copy = string.Empty;
                    continue;
                }

                Settings.Client_Source = source;
                Settings.Client_Copy = copy;

                Settings.Default.Save();
            }
        }
    }
}