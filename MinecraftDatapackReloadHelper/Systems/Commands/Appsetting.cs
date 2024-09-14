using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Systems.Control.Setting;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Appsetting : IToolCommand
    {
        public async Task Run(List<string> args)
        {
            if (args.Contains("auto"))
                await ApplicationSetting.AutoChangeRconSettingAsync();
            else
                await ApplicationSetting.ChangeRconSettingAsync();
        }
    }
}