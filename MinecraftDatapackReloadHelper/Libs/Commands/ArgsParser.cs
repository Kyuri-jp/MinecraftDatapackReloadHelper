using MinecraftDatapackReloadHelper.Libs.String;

namespace MinecraftDatapackReloadHelper.Libs.Commands
{
    internal class ArgsParser
    {
        internal static Dictionary<string, List<string>> Parse(string str, string argsPause = "--", char valueMark = '=', char valueBegin = '[', char valueClose = ']')
        {
            ArgumentNullException.ThrowIfNull(str);

            //adv trim
            str = str.Trim();

            Dictionary<string, List<string>> result = [];

            while (true)
            {
                if (str.Length <= 0)
                    break;

                string targetedArg = str;
                if (str.Contains(argsPause))
                    targetedArg = str[..str.IndexOf(argsPause, StringComparison.Ordinal)];

                if (targetedArg.Contains(valueMark))
                {
                    if (!targetedArg.Contains(valueBegin) || !targetedArg.Contains(valueClose))
                        throw new ArgumentException("Args didn't contain value beginer and closer ");
                    result.Add(targetedArg[..targetedArg.IndexOf(valueMark)].ToUpperFirst().Replace(" ", ""), [.. targetedArg[(targetedArg.IndexOf(valueBegin) + 1)..targetedArg.IndexOf(valueClose)].Split(',')]);
                }
                else
                {
                    result.Add(targetedArg.ToUpperFirst().Replace(" ", ""), []);
                }
                if (!str.Contains(argsPause))
                    break;

                str = str.Remove(0, str.IndexOf(argsPause, StringComparison.Ordinal) + argsPause.Length);
            }

            return result;
        }
    }
}