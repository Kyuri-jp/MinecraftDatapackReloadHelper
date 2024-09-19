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

        private readonly Dictionary<string, string[]> argsData = new()
        {
            {Args.Copyonly.ToString(),["Rconによるreloadコマンドの送信を行わず,コピーのみを行います","--copyonly"] }
        };

        public async Task Run(Dictionary<string, List<string>> args) => await AdvReloader.ReloadAsync(Settings.Client_Source, Settings.Client_Copy, args.ContainsKey(Args.Copyonly.ToString()));

        public Dictionary<string, string[]> GetArgs() => argsData;
    }
}