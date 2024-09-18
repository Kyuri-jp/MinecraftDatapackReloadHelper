using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Tools;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Version : IToolCommand
    {
        private enum Args
        {
            Updatecheck
        }

        public async Task Run(Dictionary<string, List<string>> args)
        {
            Console.WriteLine(Programs.GetWelcomeMessage());
            if (args.ContainsKey(Args.Updatecheck.ToString()))
                await UpdateCheck.UpdateCheckerAsync();
        }
    }
}