using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Systems.Control.Setting;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Appsetting : IToolCommand
    {
        private enum Args
        {
            Auto
        }

        public async Task Run(Dictionary<string, List<string>> args)
        {
            if (args.ContainsKey(Args.Auto.ToString()))
                await ApplicationSetting.AutoChangeRconSettingAsync();
            else
                await ApplicationSetting.ChangeRconSettingAsync();
        }
    }
}