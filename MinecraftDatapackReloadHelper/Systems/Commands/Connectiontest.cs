using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Tools;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Connectiontest : IToolCommand
    {
        public async Task Run(List<string> args) => await ConnectionTest.ConnectingTesterAsync();
    }
}