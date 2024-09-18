using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Tools;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Upload : IToolCommand, IHasArgsCommand
    {
        private static readonly DirectoryInfo? copy = Directory.GetParent(Settings.Client_Copy);

        private enum Args
        {
            Additional,
            Custompath,
            Nonclean,
            Notopen,
        };

        private readonly Dictionary<string, string[]> argsData = new()
        {
            {Args.Additional.ToString(),["生成したZipファイルに継ぎ足しで文字を加えます","--additional=[<string>]"] },
            {Args.Custompath.ToString(),["対象となるフォルダを変更します","--custompath=[<severdirectory>]"] },
            {Args.Nonclean.ToString(),["advancementフォルダなどの削除を無効化します","--nonclean"] },
            {Args.Notopen.ToString(),["圧縮し終えた後のフォルダ表示を無効化します","--notopen"] }
        };

#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
        private string source = copy.FullName;
#pragma warning restore CS8602 // null 参照の可能性があるものの逆参照です。

        public async Task Run(Dictionary<string, List<string>> args)
        {
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

        public Dictionary<string, string[]> GetArgs() => argsData;
    }
}