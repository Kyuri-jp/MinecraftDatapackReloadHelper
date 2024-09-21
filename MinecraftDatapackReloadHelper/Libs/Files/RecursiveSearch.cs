namespace MinecraftDatapackReloadHelper.Libs.Files
{
    internal class RecursiveSearch
    {
        internal static bool FileExists(string begin, string regex) => GetFiles(begin, regex).Length > 0;

        internal static bool DirectoryExists(string begin, string regex) => GetDirectories(begin, regex).Length > 0;

        internal static string[] GetFiles(string begin, string regex) => Directory.GetFiles(begin, regex, SearchOption.AllDirectories);

        internal static string[] GetDirectories(string begin, string regex) => Directory.GetDirectories(begin, regex, SearchOption.AllDirectories);

        internal static string[] GetFilesWithExtensions(string path, params string[] extensions) => Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(c => extensions.Any(extension => c.EndsWith(extension))).ToArray();
    }
}