using System.Diagnostics;

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
                    result.Add(str[begin..end].Trim());
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
                    result.Add(str[begin..^0].Trim());
                    continue;
                }

            };
            return result;
        }
    }
}