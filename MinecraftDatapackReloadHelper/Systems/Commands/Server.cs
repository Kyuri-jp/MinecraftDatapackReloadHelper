using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Libs.Files;
using MinecraftDatapackReloadHelper.Libs.Java;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Server : IToolCommand, IHasArgsCommand
    {
        private enum Args
        {
            Launch
        }

        private readonly Dictionary<string, string[]> argsData = new()
        {
            {Args.Launch.ToString(),["サーバーを起動します","--launch"] }
        };

        public Task Run(Dictionary<string, List<string>> args)
        {
            string JAVA_HOME = Environment.GetEnvironmentVariable("JAVA_HOME")!;
            Java java = new(Path.Combine(JAVA_HOME, "bin"));
            DirectoryInfo directoryInfo = new(Settings.Client_Copy);
            java.RunJarFile(Directory.GetFiles(directoryInfo.Parent.Parent.FullName, "*.*").Where(c => ".jar".Any(extension => c.EndsWith(extension)))
            .ToArray()[0], "nogui");
            return Task.CompletedTask;
        }

        public Dictionary<string, string[]> GetArgs() => argsData;
    }
}