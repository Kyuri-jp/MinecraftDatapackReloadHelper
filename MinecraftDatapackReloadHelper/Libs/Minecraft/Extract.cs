using MinecraftDatapackReloadHelper.Libs.Files;
using MinecraftDatapackReloadHelper.Libs.Files.Directories;
using MinecraftDatapackReloadHelper.Tools.Display;
using System.IO.Compression;

namespace MinecraftDatapackReloadHelper.Libs.Minecraft
{
    internal class Extract
    {
        internal static void WorldFolder(string worldFolder, string output, string fileName, bool clean = true)
        {
            if (!Directory.Exists(worldFolder))
                throw new DirectoryNotFoundException(worldFolder);

            if (!File.Exists(Path.Combine(worldFolder, "level.dat")))
                throw new FileNotFoundException(Path.Combine(worldFolder, "level.dat"));

            string tempFolder = Path.Combine(Path.GetTempPath(), fileName);

            try
            {
                if (Directory.Exists(tempFolder))
                    Directory.Delete(tempFolder, true);
            }
            catch (UnauthorizedAccessException ex)
            {
                Message.Error(ex.Message);
                Message.Error(ex.StackTrace);
            }

            DirectoryCopy.Copy(worldFolder, tempFolder, true);

            if (clean)
            {
                try
                {
                    TryDelete.File(Path.Combine(tempFolder, "level.dat_old"));
                    TryDelete.File(Path.Combine(tempFolder, "session.lock"));
                    TryDelete.File(Path.Combine(tempFolder, "spsSettings.json"));
                    TryDelete.Directory(Path.Combine(tempFolder, "advancements"), true);
                    TryDelete.Directory(Path.Combine(tempFolder, "data"), true);
                    TryDelete.Directory(Path.Combine(tempFolder, "DIM1"), true);
                    TryDelete.Directory(Path.Combine(tempFolder, "DIM-1"), true);
                    TryDelete.Directory(Path.Combine(tempFolder, "playerdata"), true);
                    TryDelete.Directory(Path.Combine(tempFolder, "poi"), true);
                    TryDelete.Directory(Path.Combine(tempFolder, "scripts"), true);
                    TryDelete.Directory(Path.Combine(tempFolder, "stats"), true);
                }
                catch (Exception ex)
                {
                    Message.Error(ex.Message);
                    Message.Error(ex.StackTrace);
                }
            }
            if (File.Exists(Path.Combine(output, fileName + ".zip")))
            {
                int i = 1;
                while (File.Exists(Path.Combine(output, fileName + $"({i}).zip")))
                    i++;

                fileName += $"({i})";
            }
            ZipFile.CreateFromDirectory(tempFolder, Path.Combine(output, fileName + ".zip"));
            Directory.Delete(tempFolder, true);
        }

        internal static void Datapacks(string datapackFolder)
        {
            foreach (var item in Directory.GetDirectories(datapackFolder))
            {
                string output = Path.Combine(Settings.Client_ExtractOutput, new DirectoryInfo(item).Name!);

                if (File.Exists(output + ".zip"))
                {
                    int i = 1;
                    while (File.Exists(output + $"({i}).zip"))
                        i++;

                    output += $"({i})";
                }

                ZipFile.CreateFromDirectory(item, output + ".zip");
            }
        }
    }
}