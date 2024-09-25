﻿using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Libs.Files;
using MinecraftDatapackReloadHelper.Libs.Java;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Server : IToolCommand, IHasArgsCommand
    {
        private enum Args
        {
            Launch,
            GetServerJava,
            GetInstalledJava
        }

        private readonly Dictionary<string, string[]> _argsData = new()
        {
            {Args.Launch.ToString(),["サーバーを起動します","--launch"] },
            {Args.GetServerJava.ToString(),["サーバーのclassバージョンを取得します","--getserverjava"] },
            {Args.GetInstalledJava.ToString(),["インストールされているJavaを表示します","--getinstalledjava"] }
        };

        Task IToolCommand.Run(Dictionary<string, List<string>> args)
        {
            if (args.ContainsKey(Args.Launch.ToString()))
            {
                int serverJavaVersion = Java.GetJarMajorVersion(Directory.GetFiles(
                    Path.Combine(
                        Path.GetDirectoryName(
                            RecursiveSearch.GetFilesWithExtensions(Settings.Copypath, extensions: ".jar")[0])!,
                        "versions"), "*.jar", SearchOption.AllDirectories)[0]) - 44;
                Dictionary<int, string> clientJavas =
                    Java.GetJavas().ToDictionary(x => ParseJavaVersion(x.Key), x => x.Value);

                Java java = new(clientJavas[serverJavaVersion]);
                Task.Run(() =>
                    java.RunJarFile(RecursiveSearch.GetFilesWithExtensions(Settings.Copypath, extensions: ".jar")[0],
                        "nogui")).Start();
                return Task.CompletedTask;
            }

            if (args.ContainsKey(Args.GetServerJava.ToString()))
            {
                Console.WriteLine($"Class Version : {Java.GetJarMajorVersion(Directory.GetFiles(
                    Path.Combine(
                        Path.GetDirectoryName(
                            RecursiveSearch.GetFilesWithExtensions(Settings.Copypath, extensions: ".jar")[0])!,
                        "versions"), "*.jar", SearchOption.AllDirectories)[0]) - 44}");
                return Task.CompletedTask;
            }

            if (args.ContainsKey(Args.GetInstalledJava.ToString()))
            {
                foreach (KeyValuePair<string, string> keyValuePair in Java.GetJavas())
                    Console.WriteLine($"{keyValuePair.Key} : {keyValuePair.Value}");
                return Task.CompletedTask;
            }

            Console.WriteLine("Please enter any args.");
            return Task.CompletedTask;
        }

        private static int ParseJavaVersion(string java) => java.StartsWith("1.") ? int.Parse([java[2]]) : int.Parse(java[..java.IndexOf('.')]);

        Dictionary<string, string[]> IHasArgsCommand.GetArgs() => _argsData;
    }
}