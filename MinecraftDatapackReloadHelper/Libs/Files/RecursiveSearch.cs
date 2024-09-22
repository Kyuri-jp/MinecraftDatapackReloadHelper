namespace MinecraftDatapackReloadHelper.Libs.Files
{
    internal class RecursiveSearch
    {
        internal static bool FileExists(string begin, string fileName) => GetFiles(begin, fileName).Length > 0;

        internal static bool DirectoryExists(string begin, string directoryName) => GetDirectories(begin, directoryName).Length > 0;

        internal static string[] GetFiles(string begin, string regex = "*")
        {
            List<string> files = [];
            FileSeatch(begin, regex, files);
            return [.. files];
        }

        internal static string[] GetDirectories(string begin, string regex = "*")
        {
            List<string> files = [];
            DirectorySeatch(begin, regex, files);
            return [.. files];
        }

        private static void FileSeatch(string begin, string regex, List<string> list)
        {
            if (Directory.GetDirectoryRoot(begin) == begin)
                return;
            Directory.GetFiles(begin, regex, SearchOption.AllDirectories).ToList().ForEach(item => list.Add(item));
            GetFiles(Directory.GetParent(begin)!.FullName, regex);
        }

        private static void DirectorySeatch(string begin, string regex, List<string> list)
        {
            if (Directory.GetDirectoryRoot(begin) == begin)
                return;
            Directory.GetDirectories(begin, regex, SearchOption.AllDirectories).ToList().ForEach(item => list.Add(item));
            GetFiles(Directory.GetParent(begin)!.FullName, regex);
        }

        internal static string[] GetFilesWithExtensions(string path, params string[] extensions) => GetFiles(path, "*.*").Where(c => extensions.Any(extension => c.EndsWith(extension))).ToArray();
    }
}