using MinecraftDatapackReloadHelper.Interfaces.Commands;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Terminal : IToolCommand
    {
        public async Task Run(List<string> args) => await Control.Terminal.RunAsync();
    }
}