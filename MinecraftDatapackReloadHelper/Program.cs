using MinecraftDatapackReloadHelper;
using MinecraftDatapackReloadHelper.API.Command;
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
            //commands
            var commands = new SortedDictionary<string, string>()
            {
                {"appsetting","Rconなどの設定を変更できます" },
                {"pathsetting","データパックのパスを変更できます" },
                {"connectiontest","Rconの接続をテストします" },
                {"reload","データパックをコピーした後、データパックを再読み込みします" },
                {"terminal","コマンドを実行できるターミナルを起動します" },
                {"showsetting","設定を表示します" },
                {"help","この文章を表示します" }
            };

            //message
            Console.WriteLine("====================\n" +
                    "Hello!\n" +
                    "This is Minecraft Datapack Reload Helper.\n" +
                    "This app is released by MIT License.\n" +
                    "Copyright (c) 2024 Kyuri\n" +
                    "Used Libraries:" +
                    "CoreRCON v5.4.1 / MIT License Copyright (c) 2017 Scott Kaye\n" +
                    "System.Configuration.ConfigurationManager v9.0.0 / MIT License Copyright (c) .NET Foundation and Contributors\n" +
                    "====================\n");

            Settings.Default.Save();
            while (true)
            {
                string? command = string.Empty;
                Console.Write("> ");

                while (command == string.Empty)
                {
                    command = Console.ReadLine();
                }

#pragma warning disable CS8604 // Null 参照引数の可能性があります。
                List<string> args = ArgsParser.Parse(command);
#pragma warning restore CS8604 // Null 参照引数の可能性があります。

                switch (args[0])
                {
                    case "appsetting":
                        await ChangeRconSetting();
                        break;

                    case "pathsetting":
                        ChangePathSetting();
                        break;

                    case "connectiontest":
                        await ConnectingTester();
                        break;

                    case "reload":
                        await AdvReloader.Reload(Settings.Client_Source, Settings.Client_Copy, args.Contains("copyonly"));
                        break;

                    case "terminal":
                        await Terminal.Run();
                        break;

                    case "showsetting":
                        Console.WriteLine($"Ip: {Settings.Rcon_IP}\n" +
                            $"Port : {Settings.Rcon_Port}\n" +
                            $"Password : {Settings.Rcon_Password}\n" +
                            $"Source : {Settings.Client_Source}\n" +
                            $"Copy : {Settings.Client_Copy}\n");
                        break;

                    case "help":
                        foreach (var str in commands)
                        {
                            string key = str.Key;
                            string value = str.Value;
                            Console.WriteLine($"{key} : {value}");
                        }
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{command} is an invalid command.");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
        }

        private static async Task ConnectingTester()
        {
            var connection = RconConnector.GetRconInst();
            try
            {
                Console.WriteLine(await connection.SendCommandAsync("say Rcon was connected"));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static async Task ChangeRconSetting()
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

            await ConnectingTester();
        }

        private static void ChangePathSetting()
        {
            string? source = string.Empty;

            while (source == string.Empty)
            {
                Console.WriteLine("Please enter source directory path.");
                Console.ForegroundColor = ConsoleColor.White;
                source = Console.ReadLine();
                if (source == ":skip")
                    break;
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

            string? copy = string.Empty;

            while (copy == string.Empty)
            {
                Console.WriteLine("Please enter copy directory path.");
                Console.ForegroundColor = ConsoleColor.White;
                copy = Console.ReadLine();
                if (copy == ":skip")
                    break;
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

                if (source != ":skip")
                    Settings.Client_Source = source;

                if (copy != ":skip")
                    Settings.Client_Copy = copy;

                Settings.Default.Save();
            }
        }
    }
}