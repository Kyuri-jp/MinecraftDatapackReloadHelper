namespace MinecraftDatapackReloadHelper.Libs.Files
{
    internal class RecursiveSearch
    {
        internal static bool FileExists(string begin, string fileName, SearchOption searchOption = SearchOption.TopDirectoryOnly) => GetFiles(begin, fileName, searchOption).Length > 0;

        internal static bool DirectoryExists(string begin, string directoryName, SearchOption searchOption = SearchOption.TopDirectoryOnly) => GetDirectories(begin, directoryName, searchOption).Length > 0;

        internal static string[] GetFiles(string begin, string regex = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            Recursive recursive = new();
            return recursive.FileSeatch(begin, regex, searchOption);
        }

        internal static string[] GetDirectories(string begin, string regex = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            Recursive recursive = new();
            return recursive.DirectorySeatch(begin, regex, searchOption);
        }

        private class Recursive
        {
            private readonly List<string> _list = [];

            internal Recursive() => _list.Clear();

            internal string[] FileSeatch(string begin, string regex, SearchOption searchOption)
            {
                if (Directory.GetDirectoryRoot(begin) == begin)
                    return [.. _list];
                Directory.GetFiles(begin, regex).ToList().ForEach(_list.Add);
                FileSeatch(Directory.GetParent(begin)!.FullName, regex, searchOption);
                return [.. _list];
            }

            internal string[] DirectorySeatch(string begin, string regex, SearchOption searchOption)
            {
                if (Directory.GetDirectoryRoot(begin) == begin)
                    return [.. _list];
                Directory.GetDirectories(begin, regex).ToList().ForEach(_list.Add);
                DirectorySeatch(Directory.GetParent(begin)!.FullName, regex, searchOption);
                return [.. _list];
            }
        }

        internal static string[] GetFilesWithExtensions(string path, SearchOption searchOption = SearchOption.TopDirectoryOnly, params string[] extensions) => GetFiles(path, "*.*", searchOption).Where(c => extensions.Any(extension => c.EndsWith(extension))).ToArray();
    }
}