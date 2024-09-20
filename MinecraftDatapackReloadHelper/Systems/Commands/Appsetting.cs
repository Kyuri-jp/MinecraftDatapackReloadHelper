using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Systems.Control.Setting;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Appsetting : IToolCommand, IHasArgsCommand
    {
        private enum Args
        {
            Auto
        }

        private readonly Dictionary<string, string[]> argsData = new()
        {
            { Args.Auto.ToString(),["選択したサーバー設定を参照し,Rconの設定を自動で行います","--auto"]}
        };

        public async Task Run(Dictionary<string, List<string>> args) =>
            await ApplicationSetting.ChangeRconSettingAsync(args.ContainsKey(Args.Auto.ToString()));

        public Dictionary<string, string[]> GetArgs() => argsData;
    }
}