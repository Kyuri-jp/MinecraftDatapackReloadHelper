using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Tools;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Version : IToolCommand
    {
        private readonly List<string> Args = ["updatecheck"];

        internal List<string> GetArgs() => Args;

        public async Task Run(List<string> args)
        {
            Console.WriteLine(Programs.GetWelcomeMessage());
            if (args.Contains("updatecheck"))
                await UpdateCheck.UpdateCheckerAsync();
        }
    }
}