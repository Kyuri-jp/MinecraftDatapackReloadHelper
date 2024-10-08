﻿using MinecraftDatapackReloadHelper.Abstract.Commands;
using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Systems.Control;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Reload : Command, IArgsable
    {
        private enum Args
        {
            Copyonly
        }

        private readonly Dictionary<string, string[]> _argsData = new()
        {
            {Args.Copyonly.ToString(),["Rconによるreloadコマンドの送信を行わず,コピーのみを行います","--copyonly"] }
        };

        internal override async Task Run(Dictionary<string, List<string>> args) => await Reloader.ReloadAsync(Settings.Sourcepath, Settings.Copypath, args.ContainsKey(Args.Copyonly.ToString()));

        Dictionary<string, string[]> IArgsable.GetArgs() => _argsData;
    }
}