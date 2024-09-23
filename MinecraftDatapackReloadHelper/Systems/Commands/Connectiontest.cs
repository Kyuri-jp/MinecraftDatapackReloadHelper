using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Systems.Control;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Connectiontest : IToolCommand
    {
        async Task IToolCommand.Run(Dictionary<string, List<string>> args) => await ConnectionTest.ConnectingTesterAsync();
    }
}