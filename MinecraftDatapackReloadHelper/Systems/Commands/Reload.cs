using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Tools;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Reload : IToolCommand
    {
        public async Task Run(List<string> args) => await AdvReloader.ReloadAsync(Settings.Client_Source, Settings.Client_Copy, args.Contains("copyonly"));
    }
}