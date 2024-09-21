using System.Text.RegularExpressions;

namespace MinecraftDatapackReloadHelper.Libs.Files
{
    internal class RecursiveFileSearcher
    {
        internal static bool RecursiveFileExists(string begin, string markerFile) => (bool)Search(begin, markerFile)[0];

        internal static string RecursiveGetDirectoryPath(string begin, string markerFile) => (string)Search(begin, markerFile)[1];

        internal static string RecursiveGetFilePath(string begin, string markerFile) => (string)Search(begin, markerFile)[2];

        private static List<object> Search(string begin, string marker)
        {
            ArgumentException.ThrowIfNullOrEmpty(begin);
            ArgumentException.ThrowIfNullOrEmpty(marker);
            DirectoryInfo directoryInfo = new(begin);
            Regex regex = new(marker);

            foreach (FileInfo file in directoryInfo.GetFiles())
                if (File.Exists(Path.Combine(begin, regex.Match(file.Name).Value)))
                    return [true, begin, regex.Match(file.Name).Value];

            string root = directoryInfo.Root.FullName;

#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
            begin = directoryInfo.Parent.FullName;
#pragma warning restore CS8602 // null 参照の可能n性があるものの逆参照です。

            if (root == begin)
            {
                return [false, begin, marker];
            }

            return Search(begin, marker);
        }
    }
}