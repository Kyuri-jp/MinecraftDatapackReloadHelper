using MinecraftDatapackReloadHelper.API.Command;
using MinecraftDatapackReloadHelper.Tools.Control.Setting;

namespace MinecraftDatapackReloadHelper.Tools.Control
{
    internal class AppCommandTerminal
    {
        //commands
        private static readonly SortedDictionary<string, string> commands = new()
        {
            {"AppSetting","Rconなどの設定を変更できます" },
            {"PathSetting","データパックのパスを変更できます" },
            {"ConnectionTest","Rconの接続をテストします" },
            {"Reload","データパックをコピーした後、データパックを再読み込みします" },
            {"Terminal","コマンドを実行できるターミナルを起動します" },
            {"ShowSetting","設定を表示します" },
            {"Upload","ワールドフォルダをZip形式に書き出します" },
            {"Help","この文章を表示します" },
            {"Version","このツールのバージョンを表示します" },
            {"Exit","このツールを終了します" }
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
                        if (args.Contains("auto"))
                        {
                            await ApplicationSetting.AutoChangeRconSettingAsync();
                            break;
                        }
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
#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
                        string source = copy.FullName;
#pragma warning restore CS8602 // null 参照の可能性があるものの逆参照です。
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
                                    Display.Console.Error("Please enter anything.");
                                    continue;
                                }

                                if (!Directory.Exists(source))
                                {
                                    Display.Console.Error($"{source} is not found.");
                                    continue;
                                }

                                if (!Directory.Exists(Path.Combine(source, "level.dat")))
                                {
                                    Display.Console.Error($"{source} is not found level.dat.\nMaybe, this directory is not world folder.");
                                    continue;
                                }
                                break;
                            }
                        }
                        await WorldUpload.Upload(source, Settings.Client_UploadOutput, !args.Contains("nonclean"), !args.Contains("notopen"), additional, parent);
                        break;

                    case "help":
                        Console.WriteLine("コマンドや引数は大文字小文字の区別はありません\n" +
                            "また、引数は -- で区切ります\n" +
                            "- は区切りとして認識されません\n" +
                            "引数についてはReadmeを参照ください -> https://github.com/Kyuri-jp/MinecraftDatapackReloadHelper");

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
                            await UpdateCheck.UpdateCheckerAsync();

                        break;

                    case "exit":
                        Environment.Exit(0);
                        break;

                    default:
                        Display.Console.Error($"{command} is an invalid command.");
                        break;
                }
            }
        }
    }
}