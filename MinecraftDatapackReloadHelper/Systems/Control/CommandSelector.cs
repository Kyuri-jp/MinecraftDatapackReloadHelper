using MinecraftDatapackReloadHelper.Systems.Commands;
using System.ComponentModel.DataAnnotations;

namespace MinecraftDatapackReloadHelper.Systems.Control
{
    internal class CommandSelector
    {
        //commands
        private static readonly Dictionary<Dictionary<string, object>, string> commandsData = new()
        {
            { new Dictionary<string,object>{{"AppSetting", new Appsetting() }},"Rconなどの設定を変更できます" },
            { new Dictionary<string,object>{{"PathSetting",new Pathsetting() }}, "データパックや出力のパスを変更できます" },
            { new Dictionary<string,object>{{"ConnectionTest",new Connectiontest() }}, "Rconの接続をテストします" },
            { new Dictionary<string,object>{{"Reload",new Reload() }}, "データパックを再読み込みさせます" },
            { new Dictionary<string,object>{{"Terminal",new Commands.Terminal() }}, "Rconを通じてコマンドを実行できるターミナルを起動します" },
            { new Dictionary<string,object>{{"ShowSetting",new Showsetting() }}, "現在の設定を表示します" },
            { new Dictionary<string,object>{{"Upload",new Upload() }}, "ワールドをZip形式で書き出します" },
            { new Dictionary<string,object>{{"Help",new Help() }}, "ヘルプを表示します" },
            { new Dictionary<string,object>{{"Version",new Commands.Version() }}, "ツールのバージョンを表示します" },
            { new Dictionary<string,object>{{"Exit",new Exit() }}, "ツールを終了します" }
        };

        internal static SortedDictionary<string, string> GetCommandHelp()
        {
            SortedDictionary<string, string> result = [];
            foreach (KeyValuePair<Dictionary<string, object>, string> keyValuePair in commandsData)
            {
                string help = keyValuePair.Value.ToString();
                Dictionary<string, object> data = keyValuePair.Key;
                foreach (KeyValuePair<string, object> keyValuePair1 in data)
                    result.Add(keyValuePair1.Key, help);
            }
            return result;
        }

        internal static async Task RunCommand(Dictionary<string, List<string>?> args)
        {
            Dictionary<string, dynamic> obj = [];
            foreach (Dictionary<string, dynamic> key in commandsData.Keys)
                foreach (var item in key)
                    obj.Add(ToUpperOnlyFirstLetter(item.Key), item.Value);

            ArgumentException.ThrowIfNullOrEmpty(args.ElementAt(0).Key);

            if (!obj.ContainsKey(ToUpperOnlyFirstLetter(args.ElementAt(0).Key)))
            {
                Tools.Display.Message.Error($"{args.ElementAt(0).Key} is an invalid command.");
                return;
            }

            if (args.Keys.Contains(string.Join("_", obj[args.ElementAt(0).Key].GetArgs())))
                await RunMethod(obj[args.ElementAt(0).Key], args);
            else
                await RunMethod(obj[args.ElementAt(0).Key]);
        }

        private static async Task RunMethod(dynamic inst, Dictionary<string, List<string>?> args) => await inst.Run(args);

        private static async Task RunMethod(dynamic inst) => await inst.Run();

        private static string ToUpperOnlyFirstLetter(string value)
        {
            char[] chars = value.ToLower().ToCharArray();
            chars[0] = char.ToUpper(chars[0]);
            return string.Join("", chars);
        }
    }
}