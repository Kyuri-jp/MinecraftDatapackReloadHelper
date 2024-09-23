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

        private readonly Dictionary<string, string[]> _argsData = new()
        {
            {Args.Launch.ToString(),["サーバーを起動します","--launch"] }
        };

        public Task Run(Dictionary<string, List<string>> args)
        {
            string javaHome = Environment.GetEnvironmentVariable("JAVA_HOME")!;
            Java java = new(Path.Combine(javaHome, "bin"));
            java.RunJarFile(RecursiveSearch.GetFilesWithExtensions(Settings.Copypath, extensions: ".jar")[0], "nogui");
            return Task.CompletedTask;
        }

        public Dictionary<string, string[]> GetArgs() => _argsData;
    }
}