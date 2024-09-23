using MinecraftDatapackReloadHelper.Libs.Console.Asker;
using MinecraftDatapackReloadHelper.Libs.Files;

namespace MinecraftDatapackReloadHelper.Systems.Commands.SettingInterface
{
    internal class PathSetting
    {
        internal static void ChangePathSetting()
        {
            string source = string.Empty;
            string copy = string.Empty;
            string upload = string.Empty;

            while (source == string.Empty)
            {
                Console.ForegroundColor = ConsoleColor.White;
                source = Asker.PathAsk("Please enter source directory path.", true);
                if (source == ":skip")
                    break;
                if (!File.Exists(Path.Combine(source, "pack.mcmeta")))
                {
                    Tools.Display.Message.Warning($"{source} is not contain pack.mcmeta.");
                    source = string.Empty;
                    continue;
                }
            }

            while (copy == string.Empty)
            {
                Console.ForegroundColor = ConsoleColor.White;
                copy = Asker.PathAsk("Please enter copy directory path.", true);
                if (copy == ":skip")
                    break;
                if (!RecursiveSearch.FileExists(copy, "level.dat"))
                {
                    Tools.Display.Message.Warning($"Not found level file in {copy}'s parents");
                    copy = string.Empty;
                    continue;
                }
                if (!RecursiveSearch.FileExists(copy, "server.properties"))
                {
                    Tools.Display.Message.Warning($"Not found server.properties in {copy}'s parents.\nMaybe this directory is not server.");
                    copy = string.Empty;
                    continue;
                }
            }

            while (upload == string.Empty)
            {
                Console.ForegroundColor = ConsoleColor.White;
                upload = Asker.PathAsk("Please enter world upload directory path.", true);
                if (upload == ":skip")
                    break;
                if (!Directory.Exists(upload))
                {
                    Tools.Display.Message.Warning($"{upload} is not exists.");
                    upload = string.Empty;
                    continue;
                }
            }
            if (source != ":skip")
                Settings.Sourcepath = source;

            if (copy != ":skip")
                Settings.Copypath = copy;

            if (upload != ":skip")
                Settings.Extractoutput = upload;

            Settings.Default.Save();
        }
    }
}