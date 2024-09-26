namespace MinecraftDatapackReloadHelper.Libs.Files.Directories
{
    internal static class DirectoryType
    {
        internal static bool IsFile(string path) => !File.GetAttributes(path).HasFlag(FileAttributes.Directory);

        internal static bool IsDirectory(string path) => File.GetAttributes(path).HasFlag(FileAttributes.Directory);

        internal static FileAttributes GetFileType(this string path)
        {
            if (IsFile(path))
                return FileAttributes.Normal;
            if (IsDirectory(path))
                return FileAttributes.Directory;
            throw new DirectoryNotFoundException();
        }
    }
}