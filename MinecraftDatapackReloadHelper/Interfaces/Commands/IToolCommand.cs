namespace MinecraftDatapackReloadHelper.Interfaces.Commands
{
    internal interface IToolCommand
    {
        internal Task Run(Dictionary<string, List<string>?> args);
    }
}