using MinecraftDatapackReloadHelper.Tools;

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
                if (!allowNull && string.IsNullOrEmpty(read))
                {
                    System.Console.WriteLine("Plase enter any value.");
                    continue;
                }
                return read ?? string.Empty;
            }
        }

        internal static string PathAsk(string message, bool directory = false, bool allowNull = false)
        {
            while (true)
            {
                FileAttributes attributes = FileAttributes.Normal;
                if (directory)
                    attributes = FileAttributes.Directory;
                string? read = Ask(message, allowNull);
                try
                {
                    if (DirectoryUtil.GetFileType(read) == attributes)
                        return read;
                    else
                    {
                        System.Console.WriteLine("The path is invalid.");
                        continue;
                    }
                }
                catch (FileNotFoundException)
                {
                    System.Console.WriteLine("The directory is not found.");
                    continue;
                }
                catch (DirectoryNotFoundException)
                {
                    System.Console.WriteLine("The directory is not found.");
                    continue;
                }
                /*catch (IOException)
                {
                    return read ?? string.Empty;
                }*/
            }
        }
    }
}