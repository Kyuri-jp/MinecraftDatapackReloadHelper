using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Libs.Console;
using MinecraftDatapackReloadHelper.Systems.Commands.SettingInterface;

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

        private readonly Dictionary<string, string[]> _argsData = new()
        {
            { Args.Auto.ToString(),["選択したサーバー設定を参照し,Rconの設定を自動で行います","--rcon --auto"]},
            { Args.Rcon.ToString(),["Rconの設定を変更します","--rcon"]},
            { Args.Path.ToString(),["パスの変更を行います","--path"]},
            { Args.Show.ToString(),["設定を表示します","--show=[<category]>"]}
        };

        private readonly Dictionary<string, Dictionary<string, string>> _settingsData = new()
        {
            { "Rcon",new Dictionary<string, string>
                {
                    {"Ip Address", Settings.Rconip } ,
                    {"Port", Settings.Rconport.ToString() } ,
                    {"Password", Settings.Rconpassword } ,
                }
            },
            { "DirectoryPath",new Dictionary<string, string>
                {
                    {"Server Side Datapack Path", Settings.Sourcepath},
                    {"Sourced Datapack Path", Settings.Copypath},
                    {"To Extract Folder", Settings.Extractoutput },
                }
            }
        };

        async Task IToolCommand.Run(Dictionary<string, List<string>> args)
        {
            if (args.ContainsKey(Args.Show.ToString()))
            {
                if (args[Args.Show.ToString()].Count <= 0)
                {
                    foreach (var category in _settingsData)
                    {
                        Console.WriteLine($"\n<{category.Key}>");
                        foreach (var setting in category.Value)
                            Console.WriteLine($"{setting.Key} : {setting.Value}");
                    }
                }
                else
                {
                    foreach (var item in _settingsData.Where(x => args[Args.Show.ToString()].Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value).Values)
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
                    Message.Warning($"Please set any args (--{Args.Rcon.ToString()},--{Args.Path.ToString()})");
            }
        }

        Dictionary<string, string[]> IHasArgsCommand.GetArgs() => _argsData;
    }
}