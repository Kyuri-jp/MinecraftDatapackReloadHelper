using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Libs.Console;
using MinecraftDatapackReloadHelper.Libs.String;

namespace MinecraftDatapackReloadHelper.Systems.Commands;

internal class Java : IToolCommand, IHasArgsCommand
{
    private enum Args
    {
        GetInstalledJava
    }

    private readonly Dictionary<string, string[]> _argsData = new()
    {
        { Args.GetInstalledJava.ToString(), ["インストールされているJavaを取得します", "--getinstalledjava"] }
    };

    public Task Run(Dictionary<string, List<string>> args)
    {
        if (args.ContainsKey(Args.GetInstalledJava.ToString().ToUpperFirst()))
        {
            foreach (KeyValuePair<string, string> keyValuePair in Libs.Java.Java.GetJavas())
                Console.WriteLine($"{keyValuePair.Key} : {keyValuePair.Value}");
            return Task.CompletedTask;
        }

        Message.Warning($"Please set any args (--{Args.GetInstalledJava.ToString()})");

        return Task.CompletedTask;
    }

    Dictionary<string, string[]> IHasArgsCommand.GetArgs() => _argsData;
}