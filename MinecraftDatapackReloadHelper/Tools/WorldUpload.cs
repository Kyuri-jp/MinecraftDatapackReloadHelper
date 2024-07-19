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

            worldFolder = RecursiveFileSearcher.RecursiveGetDirectoryPath(worldFolder, "level.dat");

            if (!File.Exists(Path.Combine(worldFolder, "level.dat")))
                throw new FileNotFoundException(Path.Combine(worldFolder, "level.dat"));

            DirectoryInfo worldFolderInfo = new(worldFolder);
            string worldFolderName = worldFolderInfo.Name;
            if (RecursiveFileSearcher.RecursiveFileExists(worldFolder, "server.properties"))
#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
                worldFolderName = Directory.GetParent(RecursiveFileSearcher.RecursiveGetDirectoryPath(worldFolder, "server.properties")).Name;
#pragma warning restore CS8602 // null 参照の可能性があるものの逆参照です。

            string tempFolder = Path.Combine(Path.GetTempPath(), worldFolderName);

            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);

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
                    Display.Console.Error(ex.Message);
                    Display.Console.Error(ex.StackTrace);
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