using System.Diagnostics;
using System.IO.Compression;

namespace MinecraftDatapackReloadHelper.Tools
{
    internal class WorldUpload
    {
        internal static Task Upload(string? worldFolder, string output, bool clean = true, bool openFolder = true, string? additional = null, bool parent = true)
        {
            string outputFolder = output ?? string.Empty;

            if (!Directory.Exists(worldFolder))
                throw new DirectoryNotFoundException(worldFolder);

            if (!File.Exists(Path.Combine(worldFolder, "level.dat")))
                throw new FileNotFoundException(Path.Combine(worldFolder, "level.dat"));

            DirectoryInfo directoryInfo = new(worldFolder);
            string temp = Path.GetTempPath();
            string folder = Path.Combine(temp, directoryInfo.Name);
            if (parent)
                folder = Path.Combine(temp, directoryInfo.Parent.Name);
            if (Directory.Exists(folder))
                Directory.Delete(folder, true);

            Console.ForegroundColor = ConsoleColor.Yellow;
            DirectoryCopy.Copy(worldFolder, folder, true);
            Console.ForegroundColor = ConsoleColor.White;

            if (clean)
            {
                try
                {
                    File.Delete(Path.Combine(folder, "level.dat_old"));
                    File.Delete(Path.Combine(folder, "session.lock"));
                    File.Delete(Path.Combine(folder, "spsSettings.json"));
                    Directory.Delete(Path.Combine(folder, "advancements"), true);
                    Directory.Delete(Path.Combine(folder, "data"), true);
                    Directory.Delete(Path.Combine(folder, "DIM1"), true);
                    Directory.Delete(Path.Combine(folder, "DIM-1"), true);
                    Directory.Delete(Path.Combine(folder, "playerdata"), true);
                    Directory.Delete(Path.Combine(folder, "poi"), true);
                    Directory.Delete(Path.Combine(folder, "scripts"), true);
                    Directory.Delete(Path.Combine(folder, "stats"), true);
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
#pragma warning disable CS8604 // Null 参照引数の可能性があります。
            output = Path.Combine(output, directoryInfo.Name) + $"{additional}";
            if (parent)
#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
                output = Path.Combine(output, directoryInfo.Parent.Name) + $"{additional}";
#pragma warning restore CS8602 // null 参照の可能性があるものの逆参照です。
#pragma warning restore CS8604 // Null 参照引数の可能性があります。

            if (File.Exists(output + ".zip"))
            {
                Console.WriteLine("Set Index");
                int i = 1;
                while (File.Exists(output + $"({i}).zip"))
                    i++;

                output += $"({i})";
            }

            ZipFile.CreateFromDirectory(folder, output + ".zip");
            Directory.Delete(folder, true);

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
