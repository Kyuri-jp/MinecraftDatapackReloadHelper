namespace MinecraftDatapackReloadHelper.Tools.Control.Setting
{
    internal class PathSetting
    {
        internal static void ChangePathSetting()
        {
            string? source = string.Empty;

            while (source == string.Empty)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Please enter source directory path.");
                source = Console.ReadLine();
                if (source == ":skip")
                    break;
                if (source == null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Null.");
                    source = string.Empty;
                    continue;
                }
                if (!Directory.Exists(source))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{source} is not exists.");
                    source = string.Empty;
                    continue;
                }
                if (!File.Exists(Path.Combine(source, "pack.mcmeta")))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{source} is not contain pack.mcmeta.");
                    source = string.Empty;
                    continue;
                }
            }

            string? copy = string.Empty;

            while (copy == string.Empty)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Please enter copy directory path.");
                copy = Console.ReadLine();
                if (copy == ":skip")
                    break;
                if (copy == null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Null.");
                    copy = string.Empty;
                    continue;
                }
                if (!Directory.Exists(copy))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{copy} is not exists.");
                    copy = string.Empty;
                    continue;
                }
                RecursiveFileSearcher recursiveFileSearcher = new();

                if (!recursiveFileSearcher.RecursiveFileExists(copy, "level.dat"))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Not found level file in {copy}'s parents");
                    copy = string.Empty;
                    continue;
                }
                if (!recursiveFileSearcher.RecursiveFileExists(copy, "server.properties"))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Not found server.properties in {copy}'s parents.\nMaybe this directory is not server.");
                    copy = string.Empty;
                    continue;
                }
            }

            string? upload = string.Empty;

            while (upload == string.Empty)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Please enter world upload directory path.");
                upload = Console.ReadLine();
                if (upload == ":skip")
                    break;
                if (upload == null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Null.");
                    upload = string.Empty;
                    continue;
                }
                if (!Directory.Exists(upload))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{upload} is not exists.");
                    upload = string.Empty;
                    continue;
                }
            }
            if (source != ":skip")
                Settings.Client_Source = source;

            if (copy != ":skip")
                Settings.Client_Copy = copy;

            if (upload != ":skip")
                Settings.Client_UploadOutput = upload;

            Settings.Default.Save();
        }
    }
}