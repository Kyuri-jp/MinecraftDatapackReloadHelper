namespace MinecraftDatapackReloadHelper.Tools
{
    internal class DirectoryUtil
    {
        internal static bool IsFile(string path) => !File.GetAttributes(path).HasFlag(FileAttributes.Directory);

        internal static bool IsDirectory(string path) => File.GetAttributes(path).HasFlag(FileAttributes.Directory);

        internal static FileAttributes GetFileType(string path)
        {
            if (IsFile(path))
                return FileAttributes.Normal;
            else if (IsDirectory(path))
                return FileAttributes.Directory;
            else
                throw new DirectoryNotFoundException();
        }
    }
}