using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Libs.Console;
using MinecraftDatapackReloadHelper.Libs.String;
using MinecraftDatapackReloadHelper.Systems.Commands;

namespace MinecraftDatapackReloadHelper.Systems.Control
{
    internal class CommandSelector
    {
        //commands
        private static readonly Dictionary<Dictionary<string, IToolCommand>, string> CommandsData = new()
        {
            { new Dictionary<string, IToolCommand>{{"Setting", new Setting() }},"Rconなどの設定を変更できます" },
            { new Dictionary<string, IToolCommand>{{"ConnectionTest",new Connectiontest() }}, "Rconの接続をテストします" },
            { new Dictionary<string, IToolCommand>{{"Reload",new Reload() }}, "データパックを再読み込みさせます" },
            { new Dictionary<string, IToolCommand>{{"Server",new Server() }}, "サーバーを操作します" },
            { new Dictionary<string, IToolCommand>{{"Terminal",new Commands.Terminal() }}, "Rconを通じてコマンドを実行できるターミナルを起動します" },
            { new Dictionary<string, IToolCommand>{{"Extract",new Extract() }}, "ワールドをZip形式で書き出します" },
            { new Dictionary<string, IToolCommand>{{"Help",new Help() }}, "ヘルプを表示します" },
            { new Dictionary<string, IToolCommand>{{"Version",new Commands.Version() }}, "ツールのバージョンを表示します" },
            { new Dictionary<string, IToolCommand>{{"Exit",new Exit() }}, "ツールを終了します" }
        };

        internal static SortedDictionary<string, string> GetCommandHelp()
        {
            SortedDictionary<string, string> result = [];
            foreach (var (data, value) in CommandsData)
            {
                foreach (KeyValuePair<string, IToolCommand> keyValuePair1 in data)
                    result.Add(keyValuePair1.Key, value);
            }
            return result;
        }

        internal static Dictionary<string, IToolCommand> GetCommandInst()
        {
            Dictionary<string, IToolCommand> obj = [];
            foreach (var item in CommandsData.Keys.SelectMany(key => key))
                obj.Add(item.Key.ToUpperFirst(), item.Value);
            return obj;
        }

        internal static async Task RunCommand(Dictionary<string, List<string>> args)
        {
            Dictionary<string, IToolCommand> obj = GetCommandInst();

            ArgumentException.ThrowIfNullOrEmpty(args.ElementAt(0).Key);

            if (!obj.ContainsKey(args.ElementAt(0).Key.ToUpperFirst()))
            {
                Message.Error($"{args.ElementAt(0).Key} is an invalid command.");
                return;
            }
            await RunMethod(obj[args.ElementAt(0).Key.ToUpperFirst()], args);
        }

        private static async Task RunMethod(IToolCommand inst, Dictionary<string, List<string>> args) => await inst.Run(args);
    }
}