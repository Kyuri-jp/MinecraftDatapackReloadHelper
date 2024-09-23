using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Libs.Files;
using MinecraftDatapackReloadHelper.Libs.Minecraft;
using System.Diagnostics;

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

        private readonly Dictionary<string, string[]> _argsData = new()
        {
            {Args.Additional.ToString(),["生成したZipファイルに継ぎ足しで文字を加えます","--additional=[<string>]"] },
            {Args.Custompath.ToString(),["対象となるフォルダを変更します","--custompath=[<severdirectory>]"] },
            {Args.Extractdatapack.ToString(),["データパックのみを圧縮します", "--extractdatapack (--custompath=[<directory>]) (--notopen)"] },
            {Args.Nonclean.ToString(),["advancementフォルダなどの削除を無効化します","--nonclean"] },
            {Args.Notopen.ToString(),["圧縮し終えた後のフォルダ表示を無効化します","--notopen"] }
        };

        Task IToolCommand.Run(Dictionary<string, List<string>> args)
        {
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
                Extract.Datapacks(datapackPath);

                Console.WriteLine("Done!");
                if (!args.ContainsKey(Args.Notopen.ToString()))
                    Process.Start("explorer.exe", Settings.Extractoutput);
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

                Extract.WorldFolder(source, Settings.Extractoutput, new DirectoryInfo(folderPath).Name + additional, !args.ContainsKey(Args.Nonclean.ToString()));
            }
            Console.WriteLine("Done!");
            if (!args.ContainsKey(Args.Notopen.ToString()))
                Process.Start("explorer.exe", Settings.Extractoutput);

            return Task.CompletedTask;
        }

        Dictionary<string, string[]> IHasArgsCommand.GetArgs() => _argsData;
    }
}