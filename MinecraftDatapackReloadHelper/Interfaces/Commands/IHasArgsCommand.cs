namespace MinecraftDatapackReloadHelper.Interfaces.Commands
{
    internal interface IHasArgsCommand
    {
        internal Dictionary<string, string[]> GetArgs();
    }
}