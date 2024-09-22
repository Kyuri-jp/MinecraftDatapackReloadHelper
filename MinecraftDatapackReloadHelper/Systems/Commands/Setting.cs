using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Systems.Commands.SettingInterface;
using System.Globalization;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Setting : IToolCommand, IHasArgsCommand
    {
        private enum Args
        {
            Auto,
            Rcon,
            Path,
            Show
        }

        private readonly Dictionary<string, string[]> argsData = new()
        {
            { Args.Auto.ToString(),["選択したサーバー設定を参照し,Rconの設定を自動で行います","--rcon --auto"]},
            { Args.Rcon.ToString(),["Rconの設定を変更します","--rcon"]},
            { Args.Path.ToString(),["パスの変更を行います","--path"]}
        };

        private readonly Dictionary<string, Dictionary<string, string>> settingsData = new()
        {
            { "Rcon",new Dictionary<string, string>
                {
                    {"Ip Address", Settings.Rcon_IP } ,
                    {"Port", Settings.Rcon_Port.ToString() } ,
                    {"Password", Settings.Rcon_Password } ,
                }
            },
            { "DirectoryPath",new Dictionary<string, string>
                {
                    {"Server Side Datapack Path", Settings.Client_Copy},
                    {"Sourced Datapack Path", Settings.Client_Source},
                    {"To Extract Folder", Settings.Client_ExtractOutput },
                }
            }
        };

        public async Task Run(Dictionary<string, List<string>> args)
        {
            if (args.ContainsKey(Args.Show.ToString()))
            {
                if (args[Args.Show.ToString()].Count <= 0)
                {
                    foreach (KeyValuePair<string, Dictionary<string, string>> category in settingsData)
                    {
                        Console.WriteLine($"\n<{category.Key}>");
                        foreach (KeyValuePair<string, string> setting in category.Value)
                            Console.WriteLine($"{setting.Key} : {setting.Value}");
                    }
                }
                else
                {
                    foreach (var item in settingsData.Where(x => args[Args.Show.ToString()].Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value).Values)
                        foreach (var key in item.Keys)
                            Console.WriteLine($"{key} : {item[key]}");
                }
            }
            else
            {
                if (args.ContainsKey(Args.Path.ToString()))
                    PathSetting.ChangePathSetting();
                if (args.ContainsKey(Args.Rcon.ToString()))
                    await ApplicationSetting.ChangeRconSettingAsync(args.ContainsKey(Args.Auto.ToString()));
                if (!args.ContainsKey(Args.Rcon.ToString()) && !args.ContainsKey(Args.Path.ToString()))
                    Tools.Display.Message.Warning("Please set any args (--rcon,--path)");
            }
        }

        public Dictionary<string, string[]> GetArgs() => argsData;
    }
}