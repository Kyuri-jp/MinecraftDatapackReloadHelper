using MinecraftDatapackReloadHelper.Libs.Files;

namespace MinecraftDatapackReloadHelper.Systems.Control.Setting
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
                    Tools.Display.Message.Warning("Null.");
                    source = string.Empty;
                    continue;
                }
                if (!Directory.Exists(source))
                {
                    Tools.Display.Message.Warning($"{source} is not exists.");
                    source = string.Empty;
                    continue;
                }
                if (!File.Exists(Path.Combine(source, "pack.mcmeta")))
                {
                    Tools.Display.Message.Warning($"{source} is not contain pack.mcmeta.");
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
                    Tools.Display.Message.Warning("Null.");
                    copy = string.Empty;
                    continue;
                }
                if (!Directory.Exists(copy))
                {
                    Tools.Display.Message.Warning($"{copy} is not exists.");
                    copy = string.Empty;
                    continue;
                }

                if (!RecursiveSearcher.FileExists(copy, "level.dat"))
                {
                    Tools.Display.Message.Warning($"Not found level file in {copy}'s parents");
                    copy = string.Empty;
                    continue;
                }
                if (!RecursiveSearcher.FileExists(copy, "server.properties"))
                {
                    Tools.Display.Message.Warning($"Not found server.properties in {copy}'s parents.\nMaybe this directory is not server.");
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
                    Console.WriteLine("Please enter anything.");
                    upload = string.Empty;
                    continue;
                }
                if (!Directory.Exists(upload))
                {
                    Tools.Display.Message.Warning($"{upload} is not exists.");
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