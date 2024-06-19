namespace MinecraftDatapackReloadHelper.API.Command
{
    internal class ArgsParser
    {
        internal static List<string> Parse(string str, char argsChar = '-')
        {
            ArgumentNullException.ThrowIfNull(str);

            List<string> result = [];
            if (!str.Contains(argsChar))
            {
                result.Add(str);
                return result;
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == argsChar)
                {
                    int end = i;
                    while (true)
                    {
                        if (str[end] != argsChar && str[end] != ' ')
                            break;

                        end--;
                    }
                    int begin = end;
                    if (str[begin] == argsChar)
                        begin--;
                    while (begin != 0)
                    {
                        if ((str[begin] == argsChar))
                        {
                            begin++;
                            break;
                        }

                        begin--;
                    }
                    end++;
                    try
                    {
                        result.Add(str[begin..end].Trim());
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Maybe, Command Args is wrong.");
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    continue;
                }

                if (i == str.Length - 1)
                {
                    string seatch = str;
                    int begin = str.Length - 1;
                    if (str[begin] == argsChar)
                        begin--;
                    while (begin != 0)
                    {
                        if ((seatch[begin] == argsChar))
                        {
                            begin++;
                            break;
                        }

                        begin--;
                    }
                    try
                    {
                        result.Add(str[begin..^0].Trim());
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Maybe, Command Args is wrong.");
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    continue;
                }
            };
            return result;
        }
    }
}