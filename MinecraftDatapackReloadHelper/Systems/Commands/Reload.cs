using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Tools;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Reload : IToolCommand
    {
        private enum Args
        {
            Copyonly
        }

        public async Task Run(Dictionary<string, List<string>> args) => await AdvReloader.ReloadAsync(Settings.Client_Source, Settings.Client_Copy, args.ContainsKey(Args.Copyonly.ToString()));
    }
}