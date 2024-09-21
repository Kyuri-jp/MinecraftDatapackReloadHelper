using MinecraftDatapackReloadHelper.Libs.Files;
using MinecraftDatapackReloadHelper.Libs.Files.Directories;
using System.Diagnostics;
using System.IO.Compression;

namespace MinecraftDatapackReloadHelper.Tools
{
    internal class WorldUpload
    {
        internal static Task Upload(string worldFolder, string output, bool clean = true, bool openFolder = true, string? additional = null)
        {
            string outputFolder = output ?? string.Empty;

            if (!Directory.Exists(worldFolder))
                throw new DirectoryNotFoundException(worldFolder);

            worldFolder = RecursiveSearch.GetDirectories(worldFolder, "level.dat")[0];
            string nameWorldFolder = worldFolder;

            if (!File.Exists(Path.Combine(worldFolder, "level.dat")))
                throw new FileNotFoundException(Path.Combine(worldFolder, "level.dat"));

            if (RecursiveSearch.FileExists(worldFolder, "server.properties"))
                nameWorldFolder = RecursiveSearch.GetDirectories(worldFolder, "server.properties")[0];

            DirectoryInfo nameWorldFolderInfo = new(nameWorldFolder);
            string worldFolderName = nameWorldFolderInfo.Name;

            string tempFolder = Path.Combine(Path.GetTempPath(), worldFolderName);

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

            Console.ForegroundColor = ConsoleColor.Yellow;
            DirectoryCopy.Copy(worldFolder, tempFolder, true);
            Console.ForegroundColor = ConsoleColor.White;

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
#pragma warning disable CS8604 // Null 参照引数の可能性があります。
            output = Path.Combine(output, worldFolderName) + $"{additional}";
#pragma warning restore CS8604 // Null 参照引数の可能性があります。

            if (File.Exists(output + ".zip"))
            {
                Console.WriteLine("Set Index");
                int i = 1;
                while (File.Exists(output + $"({i}).zip"))
                    i++;

                output += $"({i})";
            }

            ZipFile.CreateFromDirectory(tempFolder, output + ".zip");
            Directory.Delete(tempFolder, true);

            if (openFolder)
            {
                string appPath = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory(outputFolder);
                Process.Start("explorer.exe", outputFolder);
                Directory.SetCurrentDirectory(appPath);
            }
            return Task.CompletedTask;
        }
    }
}