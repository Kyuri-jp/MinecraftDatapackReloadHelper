using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Libs.Files;
using MinecraftDatapackReloadHelper.Libs.String;
using MinecraftDatapackReloadHelper.Libs.Minecraft;
using System.Text;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Server : IToolCommand, IHasArgsCommand
    {
        private enum Args
        {
            Launch,
            GetServerJava,
            Stop,
            RemoveConfig,
            Setting,
            Show
        }

        private readonly Dictionary<string, string[]> _argsData = new()
        {
            {Args.Launch.ToString(),["サーバーを起動します","--launch"] },
            {Args.GetServerJava.ToString(),["サーバーのclassバージョンを取得します","--getserverjava"] },
            {Args.Stop.ToString(),["サーバーをRcon経由で停止します","--stop"] },
            {Args.RemoveConfig.ToString(),["Javaのバージョンなどを記録したファイルを消去します","--removeconfig"] },
            {Args.Setting.ToString(),["server.propertiesを編集します","--setting"] },
            {Args.Show.ToString(),["server.propertiesの内容を表示します","--setting --show=[<value>]"] }
        };

        async Task IToolCommand.Run(Dictionary<string, List<string>> args)
        {
            string jar = RecursiveSearch.GetFilesWithExtensions(Settings.Copypath, extensions: ".jar")[0];
            if (args.ContainsKey(Args.Launch.ToString()))
            {
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

            if (args.ContainsKey(Args.RemoveConfig.ToString()))
            {
                if (File.Exists(Path.Combine(Path.GetDirectoryName(jar)!, "mdeh.ujv")))
                    File.Delete(Path.Combine(Path.GetDirectoryName(jar)!, "mdeh.ujv"));
                else
                    Console.WriteLine("Config file was not found.");
                return;
            }

            if (args.ContainsKey(Args.Setting.ToString()))
            {
                if (args.ContainsKey(Args.Show.ToString()))
                {
                    if (args[Args.Show.ToString()].Count <= 0)
                    {
                        foreach (KeyValuePair<string, string> item in ServerProperties.Parse(
                                     Path.Combine(Directory.GetParent(Settings.Copypath)!.Parent!.FullName,
                                         "server.properties")))
                            Console.WriteLine($"{item.Key} : {item.Value}");
                    }
                    else
                    {
                        foreach (var item in ServerProperties.Parse(
                                         Path.Combine(Directory.GetParent(Settings.Copypath)!.Parent!.FullName,
                                             "server.properties"))
                                     .Where(x => args[Args.Show.ToString()].Contains(x.Key))
                                     .ToDictionary(x => x.Key, x => x.Value))
                            Console.WriteLine($"{item.Key} : {item.Value}");
                    }
                }
                else
                {
                }
                return;
            }

            Console.WriteLine("Please enter any args.");
        }

        private static int ParseJavaVersion(string java) => java.StartsWith("1.") ? int.Parse([java[2]]) : int.Parse(java[..java.IndexOf('.')]);

        Dictionary<string, string[]> IHasArgsCommand.GetArgs() => _argsData;
    }
}