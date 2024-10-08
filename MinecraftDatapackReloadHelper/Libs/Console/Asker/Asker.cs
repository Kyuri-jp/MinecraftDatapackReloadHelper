﻿using MinecraftDatapackReloadHelper.Libs.Files.Directories;

namespace MinecraftDatapackReloadHelper.Libs.Console.Asker
{
    internal class Asker
    {
        internal static string Ask(string message, bool allowNull = false)
        {
            ArgumentNullException.ThrowIfNull(message);
            while (true)
            {
                System.Console.WriteLine(message);
                string? read = System.Console.ReadLine();
                if (allowNull || !string.IsNullOrEmpty(read)) return read ?? string.Empty;
                System.Console.WriteLine("Plase enter any value.");
            }
        }

        internal static string PathAsk(string message, bool directory = false, bool allowNull = false, string skipper = ":skip")
        {
            while (true)
            {
                FileAttributes attributes = FileAttributes.Normal;
                if (directory)
                    attributes = FileAttributes.Directory;
                string read = Ask(message, allowNull);
                if (read == skipper)
                    return skipper;
                try
                {
                    if (read.GetFileType() == attributes)
                        return read;
                    System.Console.WriteLine("The path is invalid.");
                }
                catch (FileNotFoundException)
                {
                    System.Console.WriteLine("The directory is not found.");
                }
                catch (DirectoryNotFoundException)
                {
                    System.Console.WriteLine("The directory is not found.");
                }
                /*catch (IOException)
                {
                    return read ?? string.Empty;
                }*/
            }
        }
    }
}