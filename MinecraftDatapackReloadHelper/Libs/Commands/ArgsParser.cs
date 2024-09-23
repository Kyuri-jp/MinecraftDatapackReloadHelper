using MinecraftDatapackReloadHelper.Tools;

namespace MinecraftDatapackReloadHelper.Libs.Commands
{
    internal class ArgsParser
    {
        internal static Dictionary<string, List<string>> Parse(string str, string argsPause = "--", char valueMark = '=', char valueBegin = '[', char valueClose = ']')
        {
            ArgumentNullException.ThrowIfNull(str);

            //adv trim
            str = str.Trim();
            str = str.Replace(" ", "");

            Dictionary<string, List<string>> result = [];

            while (true)
            {
                if (str.Length <= 0)
                    break;

                string targetedArg = str;
                if (str.Contains(argsPause))
                    targetedArg = str[0..str.IndexOf(argsPause, StringComparison.Ordinal)];

                if (targetedArg.Contains(valueMark))
                {
                    if (!targetedArg.Contains(valueBegin) || !targetedArg.Contains(valueClose))
                        throw new ArgumentException("Args didn't contain value beginer and closer ");
                    result.Add(StringUtl.ToUpperOnlyFirstLetter(targetedArg[0..targetedArg.IndexOf(valueMark)]), [.. targetedArg[(targetedArg.IndexOf(valueBegin) + 1)..targetedArg.IndexOf(valueClose)].Split(',')]);
                }
                else
                {
                    result.Add(StringUtl.ToUpperOnlyFirstLetter(targetedArg), []);
                }
                if (!str.Contains(argsPause))
                    break;

                str = str.Remove(0, str.IndexOf(argsPause, StringComparison.Ordinal) + argsPause.Length);
            }

            return result;
        }

        private static void ShowAnalyzeData(Dictionary<string, List<string>> data)
        {
            try
            {
                foreach (KeyValuePair<string, List<string>> item in data)
#pragma warning disable CS8604 // Null 参照引数の可能性があります。
                    System.Console.WriteLine($"{item.Key} / {string.Join("_", item.Value.ToList())}");
#pragma warning restore CS8604 // Null 参照引数の可能性があります。
            }
            catch (ArgumentNullException)
            {
                foreach (KeyValuePair<string, List<string>> item in data)
                    System.Console.WriteLine($"{item.Key}");
            }
        }
    }
}