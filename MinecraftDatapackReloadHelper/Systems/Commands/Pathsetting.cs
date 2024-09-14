using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Systems.Control.Setting;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Pathsetting : IToolCommand
    {
        public Task Run(Dictionary<string, List<string>> args)
        {
            PathSetting.ChangePathSetting();
            return Task.CompletedTask;
        }
    }
}