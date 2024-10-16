using MinecraftDatapackReloadHelper.Abstract.Commands;
using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Libs.Files;
using MinecraftDatapackReloadHelper.Systems.Control;
using System.Diagnostics;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Extract : Command, IArgsable
    {
        private enum Args
        {
            Additional,
            Custompath,
            Extractdatapack,
            Reload,
            Nonclean,
            Notopen,
        };

        private readonly Dictionary<string, string[]> _argsData = new()
        {
            {Args.Additional.ToString(),["生成したZipファイルに継ぎ足しで文字を加えます",$"--{Args.Additional}=[<string>]"] },
            {Args.Custompath.ToString(),["対象となるフォルダを変更します",$"--{Args.Custompath}=[<severdirectory>]"] },
            {Args.Extractdatapack.ToString(),["データパックのみを圧縮します", $"--{Args.Extractdatapack} (--{Args.Custompath}=[<directory>]) (--{Args.Notopen}"] },
            {Args.Reload.ToString(),["データパックを再読み込みした後コマンドを実行します", $"--{Args.Reload}"] },
            {Args.Nonclean.ToString(),["advancementフォルダなどの削除を無効化します",$"--{Args.Nonclean}"] },
            {Args.Notopen.ToString(),["圧縮し終えた後のフォルダ表示を無効化します",$"--{Args.Notopen}"] }
        };

        internal override async Task Run(Dictionary<string, List<string>> args)
        {
            if (args.ContainsKey(Args.Reload.ToString()))
                await Reloader.ReloadAsync(Settings.Sourcepath, Settings.Copypath, true);
            if (args.ContainsKey(Args.Extractdatapack.ToString()))
            {
                string datapackPath = Settings.Copypath;
                if (args.ContainsKey(Args.Custompath.ToString()))
                {
                    datapackPath = args[Args.Custompath.ToString()][0];
                    if (!Directory.Exists(datapackPath))
                        throw new DirectoryNotFoundException(datapackPath);
                    if (!Directory.Exists(Path.Combine(Directory.GetParent(datapackPath)!.FullName, "datapacks")))
                        if (!(Directory.GetDirectories(datapackPath, "datapacks", SearchOption.AllDirectories).Length > 0))
                            throw new DirectoryNotFoundException("datapacks");
                    datapackPath = Directory.GetDirectories(datapackPath, "datapacks", SearchOption.AllDirectories)[0];
                }
                Libs.Minecraft.Extract.Datapacks(datapackPath);
            }
            else
            {
                string source = Directory.GetParent(Settings.Copypath)!.FullName;

                string additional = string.Empty;
                string folderPath = Directory.GetParent(Settings.Copypath)!.FullName;
                if (args.ContainsKey(Args.Additional.ToString()))
                    additional = args[Args.Additional.ToString()][0];

                if (args.ContainsKey(Args.Custompath.ToString()))
                    folderPath = args[Args.Custompath.ToString()][0];

                if (!Directory.Exists(folderPath))
                    throw new DirectoryNotFoundException(folderPath);
                if (!File.Exists(Path.Combine(folderPath, "level.dat")))
                    if (!(Directory.GetFiles(folderPath, "level.dat", SearchOption.AllDirectories).Length > 0))
                        throw new FileNotFoundException("level.dat");
                folderPath = Directory.GetParent(RecursiveSearch.GetFiles(folderPath, "level.dat")[0])!.FullName;
                if (File.Exists(Path.Combine(Directory.GetParent(folderPath)!.FullName, "server.properties")))
                    folderPath = Directory.GetParent(folderPath)!.FullName;

                Libs.Minecraft.Extract.WorldFolder(source, Settings.Extractoutput, new DirectoryInfo(folderPath).Name + additional, !args.ContainsKey(Args.Nonclean.ToString()));
            }
            Console.WriteLine("Done!");
            if (!args.ContainsKey(Args.Notopen.ToString()))
                Process.Start("explorer.exe", Settings.Extractoutput);
        }

        Dictionary<string, string[]> IArgsable.GetArgs() => _argsData;
    }
}