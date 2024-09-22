using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Systems.Commands.SettingInterface;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Setting : IToolCommand, IHasArgsCommand
    {
        private enum Args
        {
            Auto,
            Rcon,
            Path
        }

        private readonly Dictionary<string, string[]> argsData = new()
        {
            { Args.Auto.ToString(),["選択したサーバー設定を参照し,Rconの設定を自動で行います","--rcon --auto"]},
            { Args.Rcon.ToString(),["Rconの設定を変更します","--rcon"]},
            { Args.Path.ToString(),["パスの変更を行います","--path"]}
        };

        public async Task Run(Dictionary<string, List<string>> args)
        {
            if (args.ContainsKey(Args.Path.ToString()))
                PathSetting.ChangePathSetting();
            if (args.ContainsKey(Args.Rcon.ToString()))
                await ApplicationSetting.ChangeRconSettingAsync(args.ContainsKey(Args.Auto.ToString()));
            if (!args.ContainsKey(Args.Rcon.ToString()) && !args.ContainsKey(Args.Path.ToString()))
                Tools.Display.Message.Warning("Please set any args (--rcon,--path)");
        }

        public Dictionary<string, string[]> GetArgs() => argsData;
    }
}