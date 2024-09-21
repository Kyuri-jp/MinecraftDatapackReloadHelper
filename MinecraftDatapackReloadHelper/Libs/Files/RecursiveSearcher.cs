namespace MinecraftDatapackReloadHelper.Libs.Files
{
    internal class RecursiveSearcher
    {
        internal static bool FileExists(string begin, string regex) => GetFiles(begin, regex).Length > 0;

        internal static bool DirectoryExists(string begin, string regex) => GetDirectories(begin, regex).Length > 0;

        internal static string[] GetFiles(string begin, string regex) => Directory.GetFiles(begin, regex, SearchOption.AllDirectories);

        internal static string[] GetDirectories(string begin, string regex) => Directory.GetDirectories(begin, regex, SearchOption.AllDirectories);
    }
}