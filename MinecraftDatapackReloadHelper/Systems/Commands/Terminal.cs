using MinecraftDatapackReloadHelper.Abstract.Commands;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Terminal : Command
    {
        internal override async Task Run(Dictionary<string, List<string>> args) => await Control.Terminal.RunAsync();
    }
}