using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Interfaces.Commands.ToolCommand;
using MinecraftDatapackReloadHelper.Systems.Control.Setting;
using MinecraftDatapackReloadHelper.Tools;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Appsetting : IHaveArgs
    {
        private readonly List<string> Args = ["auto"];

        internal List<string> GetArgs() => Args;

        public async Task Run(List<string> args)
        {
            if (args.Contains("auto"))
                await ApplicationSetting.AutoChangeRconSettingAsync();
            else
                await ApplicationSetting.ChangeRconSettingAsync();
        }

        public async Task Run() => await ApplicationSetting.ChangeRconSettingAsync();
    }

    internal class Pathsetting : IToolCommand
    {
        private readonly List<string> Args = [null];

        internal List<string> GetArgs() => Args;

        public Task Run()
        {
            PathSetting.ChangePathSetting();
            return Task.CompletedTask;
        }
    }

    internal class Connectiontest : IToolCommand
    {
        private readonly List<string> Args = [null];

        internal List<string> GetArgs() => Args;

        public async Task Run() => await ConnectionTest.ConnectingTesterAsync();
    }

    internal class Reload : IHaveArgs
    {
        private readonly List<string> Args = ["copyonly"];

        internal List<string> GetArgs() => Args;

        public async Task Run(List<string> args) => await AdvReloader.ReloadAsync(Settings.Client_Source, Settings.Client_Copy, args.Contains("copyonly"));

        public async Task Run() => await AdvReloader.ReloadAsync(Settings.Client_Source, Settings.Client_Copy);
    }

    internal class Terminal : IToolCommand
    {
        private readonly List<string> Args = [null];

        internal List<string> GetArgs() => Args;

        public async Task Run() => await Control.Terminal.RunAsync();
    }

    internal class Showsetting : IToolCommand
    {
        private readonly List<string> Args = [""];

        internal List<string> GetArgs() => Args;

        public Task Run()
        {
            Console.WriteLine($"Ip: {Settings.Rcon_IP}\n" +
                        $"Port : {Settings.Rcon_Port}\n" +
                        $"Password : {Settings.Rcon_Password}\n" +
                        $"Source : {Settings.Client_Source}\n" +
                        $"Copy : {Settings.Client_Copy}\n" +
                        $"Upload Output : {Settings.Client_UploadOutput}\n");

            return Task.CompletedTask;
        }
    }

    internal class Upload : IHaveArgs
    {
        private readonly List<string> Args = ["custompath", "additional", "nonclean", "notopen"];

        internal List<string> GetArgs() => Args;

        private static readonly DirectoryInfo? copy = Directory.GetParent(Settings.Client_Copy);

#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
        private string source = copy.FullName;
#pragma warning restore CS8602 // null 参照の可能性があるものの逆参照です。

        public async Task Run(List<string> args)
        {
            string additional = string.Empty;
            if (args.Contains("additional"))
            {
                Console.WriteLine("Please enter the additional archive file name.");
                additional = Console.ReadLine() ?? string.Empty;
            }
            if (args.Contains("custompath"))
            {
                while (true)
                {
                    Console.WriteLine("Please enter world folder path.");
                    source = Console.ReadLine() ?? string.Empty;
                    if (source == string.Empty)
                    {
                        Tools.Display.Message.Error("Please enter anything.");
                        continue;
                    }

                    if (!Directory.Exists(source))
                    {
                        Tools.Display.Message.Error($"{source} is not found.");
                        continue;
                    }

                    if (!Directory.Exists(Path.Combine(source, "level.dat")))
                    {
                        Tools.Display.Message.Error($"{source} is not found level.dat.\nMaybe, this directory is not world folder.");
                        continue;
                    }
                    break;
                }
            }
            await WorldUpload.Upload(source, Settings.Client_UploadOutput, !args.Contains("nonclean"), !args.Contains("notopen"), additional);
        }

        public async Task Run() => await WorldUpload.Upload(source, Settings.Client_UploadOutput);
    }

    internal class Help : IToolCommand
    {
        private readonly List<string> Args = [null];

        internal List<string> GetArgs() => Args;

        public Task Run()
        {
            Console.WriteLine("コマンドや引数は大文字小文字の区別はありません\n" +
                        "また、引数は -- で区切ります\n" +
                        "- は区切りとして認識されません\n" +
                        "引数についてはReadmeを参照ください -> https://github.com/Kyuri-jp/MinecraftDatapackReloadHelper");

            foreach (KeyValuePair<string, string> keyValuePair in Control.CommandSelector.GetCommandHelp())
                Console.WriteLine($"{keyValuePair.Key} : {keyValuePair.Value}");

            return Task.CompletedTask;
        }
    }

    internal class Version : IHaveArgs
    {
        private readonly List<string> Args = ["updatecheck"];

        internal List<string> GetArgs() => Args;

        public async Task Run(List<string> args)
        {
            Console.WriteLine(Programs.GetWelcomeMessage());
            if (args.Contains("updatecheck"))
                await UpdateCheck.UpdateCheckerAsync();
        }

        public Task Run()
        {
            Console.WriteLine(Programs.GetWelcomeMessage());
            return Task.CompletedTask;
        }
    }

    internal class Exit : IToolCommand
    {
        internal static List<string> GetArgs() => [null];

        public Task Run()
        {
            Environment.Exit(0);
            return Task.CompletedTask;
        }
    }
}