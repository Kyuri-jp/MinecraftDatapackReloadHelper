using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Libs.Files;
using MinecraftDatapackReloadHelper.Libs.Files.Directories;
using MinecraftDatapackReloadHelper.Tools.Display;
using MinecraftDatapackReloadHelper.Tools;
using System.IO.Compression;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Upload : IToolCommand, IHasArgsCommand
    {
        private enum Args
        {
            Additional,
            Custompath,
            Extractdatapack,
            Nonclean,
            Notopen,
        };

        private readonly Dictionary<string, string[]> argsData = new()
        {
            {Args.Additional.ToString(),["生成したZipファイルに継ぎ足しで文字を加えます","--additional=[<string>]"] },
            {Args.Custompath.ToString(),["対象となるフォルダを変更します","--custompath=[<severdirectory>]"] },
            {Args.Extractdatapack.ToString(),["データパックのみを圧縮します", "--extractdatapack (--additional=[<string>]) (--custompath=[<directory>])"] },
            {Args.Nonclean.ToString(),["advancementフォルダなどの削除を無効化します","--nonclean"] },
            {Args.Notopen.ToString(),["圧縮し終えた後のフォルダ表示を無効化します","--notopen"] }
        };

        public async Task Run(Dictionary<string, List<string>> args)
        {
            if (args.ContainsKey(Args.Extractdatapack.ToString()))
            {
                string additional = string.Empty;
                string datapackPath = Settings.Client_Copy;
                if (args.ContainsKey(Args.Additional.ToString()))
                    additional = args[Args.Additional.ToString()][0];
                if (args.ContainsKey(Args.Custompath.ToString()))
                {
                    datapackPath = args[Args.Custompath.ToString()][0];
                    if (!Directory.Exists(datapackPath))
                        throw new DirectoryNotFoundException(datapackPath);
                    if (!(Directory.GetDirectories(datapackPath, "datapacks", SearchOption.AllDirectories).Length > 0))
                        throw new DirectoryNotFoundException("datapacks");
                    datapackPath = Directory.GetDirectories(datapackPath, "datapacks", SearchOption.AllDirectories)[0];
                }
                foreach (var item in Directory.GetDirectories(datapackPath))
                {
                    string output = Path.Combine(Settings.Client_UploadOutput, new DirectoryInfo(item).Name!) + $"{additional}";

                    if (File.Exists(output + ".zip"))
                    {
                        int i = 1;
                        while (File.Exists(output + $"({i}).zip"))
                            i++;

                        output += $"({i})";
                    }

                    ZipFile.CreateFromDirectory(item, output + ".zip");
                }
            }
            else
            {
                DirectoryInfo copy = Directory.GetParent(Settings.Client_Copy)!;
                string source = copy.FullName;

                string additional = string.Empty;
                if (args.ContainsKey(Args.Additional.ToString()))
                    additional = args[Args.Additional.ToString()][0];

                if (args.ContainsKey(Args.Custompath.ToString()))
                {
                    source = args[Args.Custompath.ToString()][0];
                    if (!Directory.Exists(source))
                        throw new DirectoryNotFoundException(source);
                    if (!Directory.Exists(Path.Combine(source, "level.dat")))
                        throw new FileNotFoundException(Path.Combine(source, "level.dat"));
                }
                await WorldUpload.Upload(source, Settings.Client_UploadOutput, !args.ContainsKey(Args.Nonclean.ToString()), !args.ContainsKey(Args.Notopen.ToString()), additional);
            }
        }

        public Dictionary<string, string[]> GetArgs() => argsData;
    }
}