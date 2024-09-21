using MinecraftDatapackReloadHelper.Interfaces.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Server : IToolCommand, IHasArgsCommand
    {
        private enum Args
        {
            Launch
        }

        private readonly Dictionary<string, string[]> argsData = new()
        {
            {Args.Launch.ToString(),["サーバーを起動します","--launch"] }
        };

        public Task Run(Dictionary<string, List<string>> args)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string[]> GetArgs() => argsData;
    }
}