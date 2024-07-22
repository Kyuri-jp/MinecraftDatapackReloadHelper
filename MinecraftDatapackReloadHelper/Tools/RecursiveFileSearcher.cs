namespace MinecraftDatapackReloadHelper.Tools
{
    internal class RecursiveFileSearcher
    {
        internal static bool RecursiveFileExists(string begin, string markerFile) => (bool)Search(begin, markerFile)[0];

        internal static string RecursiveGetDirectoryPath(string begin, string markerFile) => (string)Search(begin, markerFile)[1];

        private static List<object> Search(string begin, string marker)
        {
            ArgumentException.ThrowIfNullOrEmpty(begin);
            ArgumentException.ThrowIfNullOrEmpty(marker);

            if (File.Exists(Path.Combine(begin, marker)))
            {
                return [true, begin];
            }

            DirectoryInfo directoryInfo = new(begin);

            string root = directoryInfo.Root.FullName;

#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
            begin = directoryInfo.Parent.FullName;
#pragma warning restore CS8602 // null 参照の可能n性があるものの逆参照です。

            if (root == begin)
            {
                return [false, begin];
            }

            return Search(begin, marker);
        }
    }
}