using MinecraftDatapackReloadHelper.Interfaces.Commands;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Exit : IToolCommand
    {
        public Task Run(Dictionary<string, List<string>> args)
        {
            Environment.Exit(0);
            return Task.CompletedTask;
        }
    }
}