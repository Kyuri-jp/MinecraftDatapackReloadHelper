using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Tools;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Upload : IToolCommand
    {
        private static readonly DirectoryInfo? copy = Directory.GetParent(Settings.Client_Copy);

        private enum Args
        {
            Additional,
            Custompath,
            Nonclean,
            Notopen,
        };

#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
        private string source = copy.FullName;
#pragma warning restore CS8602 // null 参照の可能性があるものの逆参照です。

        public async Task Run(Dictionary<string, List<string>> args)
        {
            string additional = string.Empty;
            if (args.ContainsKey(Args.Additional.ToString()))
                additional = args[Args.Additional.ToString()][0];

            if (args.ContainsKey("custompath"))
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
            await WorldUpload.Upload(source, Settings.Client_UploadOutput, !args.ContainsKey("nonclean"), !args.ContainsKey("notopen"), additional);
        }
    }
}