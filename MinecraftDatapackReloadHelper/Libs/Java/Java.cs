using MinecraftDatapackReloadHelper.Libs.Console;

namespace MinecraftDatapackReloadHelper.Libs.Java
{
    internal class Java(string bin)
    {
        internal void RunJarFile(string file, string arg = "")
        {
            string appDir = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(Path.GetDirectoryName(file)!);
            // ReSharper disable once IteratorMethodResultIsIgnored
            Dos.RunCommand($"\"{Path.Combine(bin, "javaw.exe")}\" -jar {file} {arg}");
            Directory.SetCurrentDirectory(appDir);
        }

        internal static int GetJarMajorVersion(string file, bool searchAllClass = false)
        {
            string currentDir = Directory.GetCurrentDirectory();
            string tempFolder = Path.Combine(Path.GetTempPath(), new DirectoryInfo(file).Name);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
            Directory.CreateDirectory(tempFolder);
            Directory.SetCurrentDirectory(tempFolder);
            Dos.RunCommand($"jar xf {file}").ShowResult();
            Directory.SetCurrentDirectory(currentDir);
            string[] classFiles = Directory.GetFiles(tempFolder, "*.class", SearchOption.AllDirectories);
            List<int> foundClassVersions = [];
            if (searchAllClass)
            {
                foundClassVersions.AddRange(from classFile in classFiles
                                            from result in Dos.RunCommand($"javap -v {classFile}")
                                            where result.Contains("major version", StringComparison.OrdinalIgnoreCase)
                                            select int.Parse(result.Split(':')[1]));
            }
            else
            {
                foundClassVersions.AddRange(from result in Dos.RunCommand($"javap -v {classFiles[0]}")
                                            where result.Contains("major version", StringComparison.OrdinalIgnoreCase)
                                            select int.Parse(result.Split(':')[1]));
            }

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