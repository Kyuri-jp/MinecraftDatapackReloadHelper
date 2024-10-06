namespace MinecraftDatapackReloadHelper.Abstract.Commands
{
    internal abstract class Command
    {
        internal abstract Task Run(Dictionary<string, List<string>> args);
    }
}