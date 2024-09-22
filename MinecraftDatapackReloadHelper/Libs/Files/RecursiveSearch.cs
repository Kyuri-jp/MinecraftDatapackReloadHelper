namespace MinecraftDatapackReloadHelper.Libs.Files
{
    internal class RecursiveSearch
    {
        internal static bool FileExists(string begin, string fileName) => GetFiles(begin, fileName).Length > 0;

        internal static bool DirectoryExists(string begin, string directoryName) => GetDirectories(begin, directoryName).Length > 0;

        internal static string[] GetFiles(string begin, string regex = "*") => Directory.GetFiles(begin, regex, SearchOption.AllDirectories);

        internal static string[] GetDirectories(string begin, string regex = "*") => Directory.GetDirectories(begin, regex, SearchOption.AllDirectories);

        internal static string[] GetFilesWithExtensions(string path, params string[] extensions) => GetFiles(path, "*.*").Where(c => extensions.Any(extension => c.EndsWith(extension))).ToArray();
    }
}