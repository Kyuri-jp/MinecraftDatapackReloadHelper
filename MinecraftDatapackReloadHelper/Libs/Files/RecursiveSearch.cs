namespace MinecraftDatapackReloadHelper.Libs.Files
{
    internal class RecursiveSearch
    {
        internal static bool FileExists(string begin, string fileName) => GetFiles(begin, fileName).Length > 0;

        internal static bool DirectoryExists(string begin, string directoryName) => GetDirectories(begin, directoryName).Length > 0;

        internal static string[] GetFiles(string begin, string regex = "*")
        {
            Recursive recursive = new();
            return recursive.FileSeatch(begin, regex);
        }

        internal static string[] GetDirectories(string begin, string regex = "*")
        {
            Recursive recursive = new();
            return recursive.DirectorySeatch(begin, regex);
        }

        private class Recursive
        {
            private readonly List<string> list = [];

            internal Recursive() => list.Clear();

            internal string[] FileSeatch(string begin, string regex)
            {
                if (Directory.GetDirectoryRoot(begin) == begin)
                    return [.. list];
                Directory.GetFiles(begin, regex).ToList().ForEach(list.Add);
                FileSeatch(Directory.GetParent(begin)!.FullName, regex);
                return [.. list];
            }

            internal string[] DirectorySeatch(string begin, string regex)
            {
                if (Directory.GetDirectoryRoot(begin) == begin)
                    return [.. list];
                Directory.GetDirectories(begin, regex).ToList().ForEach(list.Add);
                DirectorySeatch(Directory.GetParent(begin)!.FullName, regex);
                return [.. list];
            }
        }

        internal static string[] GetFilesWithExtensions(string path, params string[] extensions) => GetFiles(path, "*.*").Where(c => extensions.Any(extension => c.EndsWith(extension))).ToArray();
    }
}