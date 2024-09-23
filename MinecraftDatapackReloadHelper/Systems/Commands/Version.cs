using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Tools;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Version : IToolCommand, IHasArgsCommand
    {
        private enum Args
        {
            Updatecheck
        }

        private readonly Dictionary<string, string[]> _argsData = new()
        {
            {Args.Updatecheck.ToString(),["最新のリリースタグを確認します",$"--{Args.Updatecheck}"]}
        };

        public async Task Run(Dictionary<string, List<string>> args)
        {
            Console.WriteLine(Programs.GetWelcomeMessage());
            if (args.ContainsKey(Args.Updatecheck.ToString()))
                await UpdateCheck.UpdateCheckerAsync();
        }

        Dictionary<string, string[]> IHasArgsCommand.GetArgs() => _argsData;
    }
}