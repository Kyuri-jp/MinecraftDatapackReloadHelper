namespace MinecraftDatapackReloadHelper.Tools
{
    internal class RecursiveFileSearcher
    {
        private bool found = false;

        internal bool RecursiveFileExists(string begin, string name)
        {
            found = false;
            Search(begin, name);
            return found;
        }

        private void Search(string begin, string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(begin);
            ArgumentException.ThrowIfNullOrEmpty(name);

            if (File.Exists(Path.Combine(begin, name)))
            {
                found = true;
                return;
            }

#pragma warning disable CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
            DirectoryInfo directoryInfo = Directory.GetParent(begin);
#pragma warning restore CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
            try
            {
#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
                begin = directoryInfo.FullName;
#pragma warning restore CS8602 // null 参照の可能性があるものの逆参照です。
            }
            catch (NullReferenceException)
            {
                found = false;
                return;
            }
            Search(begin, name);
        }
    }
}