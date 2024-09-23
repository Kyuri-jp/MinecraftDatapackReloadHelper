using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Tools;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Reload : IToolCommand, IHasArgsCommand
    {
        private enum Args
        {
            Copyonly
        }

        private readonly Dictionary<string, string[]> _argsData = new()
        {
            {Args.Copyonly.ToString(),["Rconによるreloadコマンドの送信を行わず,コピーのみを行います","--copyonly"] }
        };

        public async Task Run(Dictionary<string, List<string>> args) => await AdvReloader.ReloadAsync(Settings.Sourcepath, Settings.Copypath, args.ContainsKey(Args.Copyonly.ToString()));

        public Dictionary<string, string[]> GetArgs() => _argsData;
    }
}