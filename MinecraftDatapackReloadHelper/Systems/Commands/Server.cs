using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Libs.Files;
using MinecraftDatapackReloadHelper.Libs.Java;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Server : IToolCommand, IHasArgsCommand
    {
        private enum Args
        {
            Launch
        }

        private readonly Dictionary<string, string[]> _argsData = new()
        {
            {Args.Launch.ToString(),["サーバーを起動します","--launch"] }
        };

        Task IToolCommand.Run(Dictionary<string, List<string>> args)
        {
            int serverJavaVersion = Java.GetJarMajorVersion(Directory.GetFiles(
                Path.Combine(
                    Path.GetDirectoryName(
                        RecursiveSearch.GetFilesWithExtensions(Settings.Copypath, extensions: ".jar")[0])!,
                    "versions"), "*.jar", SearchOption.AllDirectories)[0]) - 44;
            Dictionary<int, string> clientJavas = Java.GetJavas().ToDictionary(x => ParseJavaVersion(x.Key), x => x.Value);

            Java java = new(Path.Combine(clientJavas[serverJavaVersion], "bin"));
            java.RunJarFile(RecursiveSearch.GetFilesWithExtensions(Settings.Copypath, extensions: ".jar")[0], "nogui");
            return Task.CompletedTask;
        }

        private static int ParseJavaVersion(string java) => java.StartsWith("1.") ? int.Parse([java[2]]) : int.Parse(java[..java.IndexOf('.')]);

        Dictionary<string, string[]> IHasArgsCommand.GetArgs() => _argsData;
    }
}