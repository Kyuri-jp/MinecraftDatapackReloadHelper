using MinecraftDatapackReloadHelper.Abstract.Commands;
using MinecraftDatapackReloadHelper.Interfaces.Commands;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Rcon : Command, IArgsable
    {
        private enum Args
        {
            GeneratePassword
        }

        private readonly Dictionary<string, string[]> _argsData = new()
        {
            { Args.GeneratePassword.ToString(), ["パスワードをランダムに設定します+", "--generatepassword"] }
        };

        internal override Task Run(Dictionary<string, List<string>> args)
        {
            throw new NotImplementedException();
        }

        Dictionary<string, string[]> IArgsable.GetArgs() => _argsData;
    }
}