using MinecraftDatapackReloadHelper.Abstract.Commands;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Exit : Command
    {
        internal override Task Run(Dictionary<string, List<string>> args)
        {
            Environment.Exit(0);
            return Task.CompletedTask;
        }
    }
}