﻿using MinecraftDatapackReloadHelper.Interfaces.Commands;
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
                string source = Path.GetDirectoryName(RecursiveSearch.GetFiles(Settings.Client_Copy, "pack.mcmeta")[0])!;
                string additional = string.Empty;
                if (args.ContainsKey(Args.Additional.ToString()))
                    additional = args[Args.Additional.ToString()][0];
                if (args.ContainsKey(Args.Custompath.ToString()))
                {
                    source = args[Args.Custompath.ToString()][0];
                    if (!Directory.Exists(source))
                        throw new DirectoryNotFoundException(source);
                    if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(RecursiveSearch.GetFiles(source, "pack.mcmeta")[0])!, "level.dat")))
                        throw new FileNotFoundException(Path.Combine(source, "pack.mcmeta"));
                }
                string fileName = source;
                if (RecursiveSearch.FileExists(source, "server.properties"))
                    fileName = Path.GetDirectoryName(RecursiveSearch.GetFiles(source, "server.properties")[0])!;

                string output = Path.Combine(Settings.Client_UploadOutput, Path.GetDirectoryName(fileName)!) + $"{additional}";

                if (File.Exists(output + ".zip"))
                {
                    Console.WriteLine("Set Index");
                    int i = 1;
                    while (File.Exists(output + $"({i}).zip"))
                        i++;

                    output += $"({i})";
                }

                ZipFile.CreateFromDirectory(source, output + ".zip");
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