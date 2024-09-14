using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Tools;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Connectiontest : IToolCommand
    {
        public async Task Run(Dictionary<string, List<string>> args) => await ConnectionTest.ConnectingTesterAsync();
    }
}