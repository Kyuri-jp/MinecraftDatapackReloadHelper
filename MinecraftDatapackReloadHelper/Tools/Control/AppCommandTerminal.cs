using MinecraftDatapackReloadHelper.API.Command;
using MinecraftDatapackReloadHelper.Tools.Control.Setting;

namespace MinecraftDatapackReloadHelper.Tools.Control
{
    internal class AppCommandTerminal
    {
        //commands
        private static readonly SortedDictionary<string, string> commands = new()
        {
            {"appsetting","Rconなどの設定を変更できます" },
            {"pathsetting","データパックのパスを変更できます" },
            {"connectiontest","Rconの接続をテストします" },
            {"reload","データパックをコピーした後、データパックを再読み込みします" },
            {"terminal","コマンドを実行できるターミナルを起動します" },
            {"showsetting","設定を表示します" },
            {"upload","ワールドフォルダをZip形式に書き出します" },
            {"help","この文章を表示します" },
            {"version","このツールのバージョンを表示します" },
            {"exit","このツールを終了します" }
        };

        internal static async Task Run()
        {
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
                        await ApplicationSetting.ChangeRconSettingAsync();
                        break;

                    case "pathsetting":
                        PathSetting.ChangePathSetting();
                        break;

                    case "connectiontest":
                        await ConnectionTest.ConnectingTesterAsync();
                        break;

                    case "reload":
                        await AdvReloader.ReloadAsync(Settings.Client_Source, Settings.Client_Copy, args.Contains("copyonly"));
                        break;

                    case "terminal":
                        await Terminal.RunAsync();
                        break;

                    case "showsetting":
                        Console.WriteLine($"Ip: {Settings.Rcon_IP}\n" +
                            $"Port : {Settings.Rcon_Port}\n" +
                            $"Password : {Settings.Rcon_Password}\n" +
                            $"Source : {Settings.Client_Source}\n" +
                            $"Copy : {Settings.Client_Copy}\n" +
                            $"Upload Output : {Settings.Client_UploadOutput}\n");
                        break;

                    case "upload":
                        DirectoryInfo? copy = Directory.GetParent(Settings.Client_Copy);
                        string source = copy.FullName;
                        string additional = string.Empty;
                        if (args.Contains("additional"))
                        {
                            Console.WriteLine("Please enter the additional archive file name.");
                            additional = Console.ReadLine() ?? string.Empty;
                        }
                        bool parent = true;
                        if (args.Contains("custompath"))
                        {
                            parent = false;
                            while (true)
                            {
                                Console.WriteLine("Please enter world folder path.");
                                source = Console.ReadLine() ?? string.Empty;
                                if (source == string.Empty)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Please enter anything.");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    continue;
                                }

                                if (!Directory.Exists(source))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"{source} is not found.");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    continue;
                                }

                                if (!Directory.Exists(Path.Combine(source, "level.dat")))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"{source} is not found level.dat.\nMaybe, this directory is not world folder.");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    continue;
                                }
                                break;
                            }
                        }
                        await WorldUpload.Upload(source, Settings.Client_UploadOutput, !args.Contains("nonclean"), !args.Contains("notopen"), additional, parent);
                        break;

                    case "help":
                        foreach (var str in commands)
                        {
                            string key = str.Key;
                            string value = str.Value;
                            Console.WriteLine($"{key} : {value}");
                        }
                        break;

                    case "version":
                        Console.WriteLine(Programs.GetWelcomeMessage());
                        if (args.Contains("updatecheck"))
                        {
                            await UpdateCheck.UpdateCheckerAsync();
                        }
                        break;

                    case "exit":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{command} is an invalid command.");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
        }
    }
}