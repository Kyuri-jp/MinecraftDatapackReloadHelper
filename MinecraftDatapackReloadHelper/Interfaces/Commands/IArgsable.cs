namespace MinecraftDatapackReloadHelper.Interfaces.Commands
{
    internal interface IArgsable
    {
        internal Dictionary<string, string[]> GetArgs();
    }
}