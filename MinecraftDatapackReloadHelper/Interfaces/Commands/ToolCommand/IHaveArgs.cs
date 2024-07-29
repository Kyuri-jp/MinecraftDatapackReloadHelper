namespace MinecraftDatapackReloadHelper.Interfaces.Commands.ToolCommand
{
    internal interface IHaveArgs : IToolCommand
    {
        protected Task Run(List<string> args);
    }
}