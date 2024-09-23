using MinecraftDatapackReloadHelper.Interfaces.Commands;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Terminal : IToolCommand
    {
        async Task IToolCommand.Run(Dictionary<string, List<string>> args) => await Control.Terminal.RunAsync();
    }
}