using MinecraftDatapackReloadHelper.Interfaces.Commands;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Showsetting : IToolCommand
    {
        public Task Run(List<string> args)
        {
            Console.WriteLine($"Ip: {Settings.Rcon_IP}\n" +
                        $"Port : {Settings.Rcon_Port}\n" +
                        $"Password : {Settings.Rcon_Password}\n" +
                        $"Source : {Settings.Client_Source}\n" +
                        $"Copy : {Settings.Client_Copy}\n" +
                        $"Upload Output : {Settings.Client_UploadOutput}\n");

            return Task.CompletedTask;
        }
    }
}