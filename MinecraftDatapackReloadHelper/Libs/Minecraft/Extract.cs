using MinecraftDatapackReloadHelper.Libs.Files.Directories;
using System.IO.Compression;

namespace MinecraftDatapackReloadHelper.Tools.Minecraft
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
                Display.Message.Error(ex.Message);
                Display.Message.Error(ex.StackTrace);
            }

            DirectoryCopy.Copy(worldFolder, tempFolder, true);

            if (clean)
            {
                try
                {
                    File.Delete(Path.Combine(tempFolder, "level.dat_old"));
                    File.Delete(Path.Combine(tempFolder, "session.lock"));
                    File.Delete(Path.Combine(tempFolder, "spsSettings.json"));
                    Directory.Delete(Path.Combine(tempFolder, "advancements"), true);
                    Directory.Delete(Path.Combine(tempFolder, "data"), true);
                    Directory.Delete(Path.Combine(tempFolder, "DIM1"), true);
                    Directory.Delete(Path.Combine(tempFolder, "DIM-1"), true);
                    Directory.Delete(Path.Combine(tempFolder, "playerdata"), true);
                    Directory.Delete(Path.Combine(tempFolder, "poi"), true);
                    Directory.Delete(Path.Combine(tempFolder, "scripts"), true);
                    Directory.Delete(Path.Combine(tempFolder, "stats"), true);
                }
                catch (FileNotFoundException)
                {
                    //Pass
                }
                catch (DirectoryNotFoundException)
                {
                    //Pass
                }
                catch (Exception ex)
                {
                    Display.Message.Error(ex.Message);
                    Display.Message.Error(ex.StackTrace);
                }
            }
            ZipFile.CreateFromDirectory(tempFolder, Path.Combine(output, fileName, ".zip"));
            Directory.Delete(tempFolder, true);
        }

        internal static void Datapacks(string datapackFolder)
        {
            foreach (var item in Directory.GetDirectories(datapackFolder))
            {
                string output = Path.Combine(Settings.Client_UploadOutput, new DirectoryInfo(item).Name!);

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