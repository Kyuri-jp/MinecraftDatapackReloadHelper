using MinecraftDatapackReloadHelper.Interfaces.Commands;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Terminal : IToolCommand
    {
        public async Task Run(Dictionary<string, List<string>?> args) => await Control.Terminal.RunAsync();
    }
}