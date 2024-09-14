using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Systems.Control.Setting;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Appsetting : IToolCommand
    {
        public async Task Run(Dictionary<string, List<string>> args)
        {
            if (args.ContainsKey("auto"))
                await ApplicationSetting.AutoChangeRconSettingAsync();
            else
                await ApplicationSetting.ChangeRconSettingAsync();
        }
    }
}