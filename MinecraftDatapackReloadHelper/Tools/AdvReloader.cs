﻿using MinecraftDatapackReloadHelper.API.Rcon;

namespace MinecraftDatapackReloadHelper.Tools
{
    internal class AdvReloader
    {
        internal static async Task ReloadAsync(string source, string copy, bool copyOnly)
        {
            // ex
            bool exceptioned = false;

            //inst
            var connection = RconConnector.GetRconInst();

            //test
            try
            {
                if (!copyOnly)
                    await connection.SendCommandAsync("say Copying Files...");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ForegroundColor = ConsoleColor.White;
                exceptioned = true;
            }

            if (!exceptioned)
            {
                //copy
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Copying...");
                var dir = new DirectoryInfo(source);
                CopyDirectory(source, Path.Combine(copy, dir.Name), true);
                Console.ForegroundColor = ConsoleColor.White;

                //reload
                if (!copyOnly)
                    Console.WriteLine(await connection.SendCommandAsync("reload"));
            }
        }

        // ref by https://learn.microsoft.com/ja-jp/dotnet/standard/io/how-to-copy-directories
        private static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            //If directory exists,delete it
            if (Directory.Exists(destinationDir))
                Directory.Delete(destinationDir, true);

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                try
                {
                    file.CopyTo(targetFilePath);
                }
                catch (IOException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine($"Copyed {targetFilePath}");
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }
    }
}