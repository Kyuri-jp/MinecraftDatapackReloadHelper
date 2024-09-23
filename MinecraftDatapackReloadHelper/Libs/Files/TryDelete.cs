namespace MinecraftDatapackReloadHelper.Libs.Files
{
    internal class TryDelete
    {
        internal static bool File(string path)
        {
            try
            {
                System.IO.File.Delete(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static bool Directory(string path, bool recrusive = false)
        {
            try
            {
                System.IO.Directory.Delete(path, recrusive);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}