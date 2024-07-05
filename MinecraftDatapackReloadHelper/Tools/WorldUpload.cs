using System.Diagnostics;
using System.IO.Compression;

namespace MinecraftDatapackReloadHelper.Tools
{
    internal class WorldUpload
    {
        internal static Task Upload(string? worldFolder, string output, bool clean = true, bool openFolder = true, string? additional = null)
        {
            if (!Directory.Exists(worldFolder))
                throw new DirectoryNotFoundException(worldFolder);

            if (!File.Exists(Path.Combine(worldFolder, "level.dat")))
                throw new FileNotFoundException(Path.Combine(worldFolder, "level.dat"));

            DirectoryInfo directoryInfo = new(worldFolder);
            string temp = Path.GetTempPath();
            string folder = Path.Combine(temp, directoryInfo.Name);
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
                    Directory.Delete(Path.Combine(folder, "advancements"));
                    Directory.Delete(Path.Combine(folder, "data"));
                    Directory.Delete(Path.Combine(folder, "DIM1"));
                    Directory.Delete(Path.Combine(folder, "DIM-1"));
                    Directory.Delete(Path.Combine(folder, "playerdata"));
                    Directory.Delete(Path.Combine(folder, "poi"));
                    Directory.Delete(Path.Combine(folder, "scripts"));
                    Directory.Delete(Path.Combine(folder, "stats"));
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
            output = Path.Combine(output, directoryInfo.Name) + $"{additional}";

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
                Process.Start(output);
            return Task.CompletedTask;
        }
    }
}
