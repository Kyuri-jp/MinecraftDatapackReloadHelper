using System.Diagnostics;

namespace MinecraftDatapackReloadHelper.Libs.Java
{
    internal class Java
    {
        private readonly string bin;

        internal Java(string bin) => this.bin = bin;

        internal void RunJarFile(string file, string arg = "")
        {
            Process process = Process.Start(Path.Combine(bin, "java.exe"), $"-jar {file} {arg}");
            process.WaitForExitAsync();
        }
    }
}