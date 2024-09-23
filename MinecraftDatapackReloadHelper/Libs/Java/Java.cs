using System.Diagnostics;
using MinecraftDatapackReloadHelper.Libs.Console;

namespace MinecraftDatapackReloadHelper.Libs.Java
{
    internal class Java
    {
        private readonly string _bin;

        internal Java(string bin) => _bin = bin;

        internal void RunJarFile(string file, string arg = "")
        {
            System.Console.WriteLine(GetJarMajorVersion(file));
            Process process = Process.Start(Path.Combine(_bin, "javaw.exe"), $"-jar {file} {arg}");
            process.WaitForExitAsync();
        }

        internal int GetJarMajorVersion(string file)
        {
            string currentDir = Directory.GetCurrentDirectory();
            string tempFolder = Path.Combine(Path.GetTempPath(), new DirectoryInfo(file).Name);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
            Directory.CreateDirectory(tempFolder);
            Directory.SetCurrentDirectory(tempFolder);
            Process.Start(Path.Combine(_bin, "jar.exe"), $"xf {file}").WaitForExit();
            Directory.SetCurrentDirectory(currentDir);
            string[] classFiles = Directory.GetFiles(tempFolder, "*.class", SearchOption.AllDirectories);
            List<int> foundClassVersions = [];
            foundClassVersions.AddRange(from classFile in classFiles from result in Dos.RunCommand($"\"{Path.Combine(_bin, "javap.exe")}\" -v {classFile}") where result.Contains("major version", StringComparison.OrdinalIgnoreCase) select int.Parse(result.Split(':')[1]));
            return foundClassVersions.Max();
        }
    }
}