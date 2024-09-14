namespace MinecraftDatapackReloadHelper.Interfaces.Commands
{
    internal interface IToolCommand
    {
        internal Task Run(List<string> args);
    }
}