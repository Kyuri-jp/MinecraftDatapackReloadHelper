using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Libs.Files;
using MinecraftDatapackReloadHelper.Libs.String;
using System.Text;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Server : IToolCommand, IHasArgsCommand
    {
        private enum Args
        {
            Launch,
            GetServerJava,
            Stop
        }

        private readonly Dictionary<string, string[]> _argsData = new()
        {
            {Args.Launch.ToString(),["サーバーを起動します","--launch"] },
            {Args.GetServerJava.ToString(),["サーバーのclassバージョンを取得します","--getserverjava"] },
            {Args.Stop.ToString(),["サーバーをRcon経由で停止します","--stop"] }
        };

        async Task IToolCommand.Run(Dictionary<string, List<string>> args)
        {
            if (args.ContainsKey(Args.Launch.ToString()))
            {
                Console.WriteLine("Searching Server Jar...");
                string jar = RecursiveSearch.GetFilesWithExtensions(Settings.Copypath, extensions: ".jar")[0];
                Console.WriteLine("Getting java version of server...");
                int serverJavaVersion;
                if (File.Exists(Path.Combine(Path.GetDirectoryName(jar)!, "mdeh.ujv")))
                    serverJavaVersion = int.Parse((await File.ReadAllLinesAsync(Path.Combine(Path.GetDirectoryName(jar)!, "mdeh.ujv")))[0].Split('=')[1]);
                else
                {
                    Console.WriteLine("Config file was not found.");
                    Console.WriteLine("Get java version from server file...");
                    serverJavaVersion = Libs.Java.Java.GetJarMajorVersion(Directory.GetFiles(
                        Path.Combine(Path.GetDirectoryName(
                            RecursiveSearch.GetFilesWithExtensions(Settings.Copypath, extensions: ".jar")[0])!,
                        "versions"), "*.jar", SearchOption.AllDirectories)[0]) - 44;
                    await using var fileStream = File.Create(Path.Combine(Path.GetDirectoryName(jar)!, "mdeh.ujv"));
                    byte[] info = new UTF8Encoding().GetBytes($"UsingJavaVersion = {serverJavaVersion}");
                    fileStream.Write(info, 0, info.Length);
                }

                Console.WriteLine("Searching client java...");
                Dictionary<int, string> clientJavas =
                    Libs.Java.Java.GetJavas().ToDictionary(x => ParseJavaVersion(x.Key), x => x.Value);

                Libs.Java.Java java = new(clientJavas[serverJavaVersion]);
                await Task.Run(() =>
                    java.RunJarFile(jar, "nogui"));
                Console.WriteLine("Turned on server!");
                return;
            }

            if (args.ContainsKey(Args.GetServerJava.ToString().ToUpperFirst()))
            {
                Console.WriteLine($"Class Version : {Libs.Java.Java.GetJarMajorVersion(Directory.GetFiles(
                    Path.Combine(
                        Path.GetDirectoryName(
                            RecursiveSearch.GetFilesWithExtensions(Settings.Copypath, extensions: ".jar")[0])!,
                        "versions"), "*.jar", SearchOption.AllDirectories)[0]) - 44}");
                return;
            }

            if (args.ContainsKey(Args.Stop.ToString()))
            {
                await Libs.Network.Rcon.RconInterfaces.SendCommandAsync("stop");
                return;
            }

            Console.WriteLine("Please enter any args.");
        }

        private static int ParseJavaVersion(string java) => java.StartsWith("1.") ? int.Parse([java[2]]) : int.Parse(java[..java.IndexOf('.')]);

        Dictionary<string, string[]> IHasArgsCommand.GetArgs() => _argsData;
    }
}