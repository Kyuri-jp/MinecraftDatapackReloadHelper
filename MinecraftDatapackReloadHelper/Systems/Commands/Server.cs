using MinecraftDatapackReloadHelper.Abstract.Commands;
using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Libs.Console.Asker;
using MinecraftDatapackReloadHelper.Libs.Files;
using MinecraftDatapackReloadHelper.Libs.Minecraft;
using MinecraftDatapackReloadHelper.Libs.String;
using System.Text;
using MinecraftDatapackReloadHelper.Libs.Console;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Server : Command, IArgsable
    {
        private enum Args
        {
            InvokeConfig,
            RemoveConfig,
            Setting,
            Show
        }

        private readonly Dictionary<string, string[]> _argsData = new()
        {
            {Args.InvokeConfig.ToString(),["コンフィグファイルを無視してサーバーを起動します","--invokeconfig"] },
            {Args.RemoveConfig.ToString(),["Javaのバージョンなどを記録したファイルを消去します","--removeconfig"] },
            {Args.Setting.ToString(),["server.propertiesを編集します","--setting"] },
            {Args.Show.ToString(),["server.propertiesの内容を表示します","--setting --show=[<value>]"] }
        };

        internal override async Task Run(Dictionary<string, List<string>> args)
        {
            string jar = RecursiveSearch.GetFilesWithExtensions(Settings.Copypath, extensions: ".jar")[0];
            if (args.ContainsKey(Args.RemoveConfig.ToString().ToUpperFirst()))
            {
                if (File.Exists(Path.Combine(Path.GetDirectoryName(jar)!, "mdrh.ujv")))
                    File.Delete(Path.Combine(Path.GetDirectoryName(jar)!, "mdrh.ujv"));
                else
                    Console.WriteLine("Config file was not found.");
                return;
            }

            if (args.ContainsKey(Args.Setting.ToString()))
            {
                Dictionary<string, string> fileData = ServerProperties.Parse(
                    Path.Combine(Directory.GetParent(Settings.Copypath)!.Parent!.FullName,
                        "server.properties"));
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
                    if (!(args[Args.Setting.ToString()].Count > 0))
                    {
                        Message.Warning("Please set any keys of properties");
                        return;
                    }

                    Dictionary<string, string> propertiesData = [];
                    foreach (var item in args[Args.Setting.ToString()])
                    {
                        if (!fileData.ContainsKey(item))
                        {
                            Message.Warning($"key of {item} was not found. Skip the progress of to set ");
                            continue;
                        }
                        propertiesData.Add(item, Asker.Ask($"Please enter {item} value."));
                    }

                    ServerProperties.Write(Path.Combine(Directory.GetParent(Settings.Copypath)!.Parent!.FullName,
                        "server.properties"), propertiesData);
                }
                return;
            }

            Console.WriteLine("Getting java version of server...");
            int serverJavaVersion;
            if (!args.ContainsKey(Args.InvokeConfig.ToString().ToUpperFirst()) && File.Exists(Path.Combine(Path.GetDirectoryName(jar)!, "mdrh.ujv")))
                serverJavaVersion = int.Parse((await File.ReadAllLinesAsync(Path.Combine(Path.GetDirectoryName(jar)!, "mdrh.ujv")))[0].Split('=')[1]);
            else
            {
                Console.WriteLine("Config file was not found.");
                Console.WriteLine("Get java version from server file...");
                serverJavaVersion = Libs.Java.Java.GetJarMajorVersion(Directory.GetFiles(
                    Path.Combine(Path.GetDirectoryName(
                        RecursiveSearch.GetFilesWithExtensions(Settings.Copypath, extensions: ".jar")[0])!,
                    "versions"), "*.jar", SearchOption.AllDirectories)[0]) - 44;
                await using var fileStream = File.Create(Path.Combine(Path.GetDirectoryName(jar)!, "mdrh.ujv"));
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
        }

        private static int ParseJavaVersion(string java) => java.StartsWith("1.") ? int.Parse([java[2]]) : int.Parse(java[..java.IndexOf('.')]);

        Dictionary<string, string[]> IArgsable.GetArgs() => _argsData;
    }
}