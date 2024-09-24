using MinecraftDatapackReloadHelper.Libs.Console;
using System.Diagnostics;

namespace MinecraftDatapackReloadHelper.Libs.Java
{
    internal class Java
    {
        private readonly string _bin;

        internal Java(string bin) => _bin = bin;

        internal void RunJarFile(string file, string arg = "")
        {
            foreach (KeyValuePair<string, string> keyValuePair in GetJavas())
                System.Console.WriteLine($"{keyValuePair.Key} : {keyValuePair.Value}");
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
            foundClassVersions.AddRange(from classFile in classFiles
                                        from result in Dos.RunCommand($"\"{Path.Combine(_bin, "javap.exe")}\" -v {classFile}")
                                        where result.Contains("major version", StringComparison.OrdinalIgnoreCase)
                                        select int.Parse(result.Split(':')[1]));
            return foundClassVersions.Max();
        }

        internal static Dictionary<string, string> GetJavas()
        {
            Dictionary<string, string> foundJavaDirectory = [];
            foreach (string exeFile in Directory.GetFiles(
                         Directory.GetParent(Environment.GetEnvironmentVariable("JAVA_HOME")!)!.FullName,
                         "java.exe",
                         SearchOption.AllDirectories))
            {
                if (ReleaseParse(Directory.GetParent(exeFile)!.Parent!.FullName).TryGetValue("JAVA_VERSION", out string? value))
                    foundJavaDirectory.Add(value, Directory.GetParent(exeFile)!.FullName);
            }

            return foundJavaDirectory;
        }

        internal static Dictionary<string, string> ReleaseParse(string javaFolder)
        {
            Dictionary<string, string> releaseInfo = [];
            if (!File.Exists(Path.Combine(javaFolder, "release")))
                return releaseInfo;
            foreach (var item in File.ReadAllLines(Path.Combine(javaFolder, "release")))
            {
                if (item[0] == '#') continue;
                releaseInfo.Add(item[..item.IndexOf('=')], item[(item.IndexOf('=') + 1)..].Replace('"', ' '));
            }

            return releaseInfo;
        }
    }
}