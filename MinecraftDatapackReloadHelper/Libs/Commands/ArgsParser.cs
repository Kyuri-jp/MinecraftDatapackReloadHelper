namespace MinecraftDatapackReloadHelper.Libs.Command
{
    internal class ArgsParser
    {
        internal static Dictionary<string, List<string>?> Parse(string str, string argsPause = "--", char valueMark = '=', char valueBegin = '[', char valueClose = ']')
        {
            ArgumentNullException.ThrowIfNull(str);

            //adv trim
            str = str.Trim();
            str = str.Replace(" ", "");
            str = str.ToLower();

            Dictionary<string, List<string>?> result = [];

            while (true)
            {
                if (str.Length <= 0)
                    break;

                if (!str.Contains(argsPause))
                {
                    result.Add(str, null);
                    return result;
                }
                ;
                if (str[0..(str.IndexOf(argsPause))].Contains(valueMark))
                {
                    if (!str[0..(str.IndexOf(argsPause))].Contains(valueBegin) || !str[0..(str.IndexOf(argsPause))].Contains(valueClose))
                        throw new ArgumentException("Args didn't contain value beginer and closer ");
                }
                str = str.Remove(0, str.IndexOf(argsPause) + argsPause.Length);
            }
            return result;
        }

        private static void ShowAnalyzeData(List<string> data)
        {
            foreach (var item in data)
                Console.WriteLine(item);
        }
    }
}