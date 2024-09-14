using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Tools;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Version : IToolCommand
    {
        private readonly List<string> Args = ["updatecheck"];

        internal List<string> GetArgs() => Args;

        public async Task Run(Dictionary<string, List<string>> args)
        {
            Console.WriteLine(Programs.GetWelcomeMessage());
            if (args.ContainsKey("updatecheck"))
                await UpdateCheck.UpdateCheckerAsync();
        }
    }
}