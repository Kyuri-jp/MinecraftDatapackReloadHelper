using System.Diagnostics;

namespace MinecraftDatapackReloadHelper.Libs.Java
{
    internal class Java
    {
        private readonly string _bin;

        internal Java(string bin) => _bin = bin;

        internal void RunJarFile(string file, string arg = "")
        {
            Process process = Process.Start(Path.Combine(_bin, "java.exe"), $"-jar {file} {arg}");
            process.WaitForExitAsync();
        }
    }
}