using MinecraftDatapackReloadHelper.Abstract.Commands;
using MinecraftDatapackReloadHelper.Systems.Control;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Connectiontest : Command
    {
        internal override async Task Run(Dictionary<string, List<string>> args) => await ConnectionTest.ConnectingTesterAsync();
    }
}