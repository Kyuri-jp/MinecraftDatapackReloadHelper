namespace MinecraftDatapackReloadHelper.API.Command
{
    internal class ArgsParser
    {
        internal static List<string> Parse(string str, string argsPause = "--")
        {
            ArgumentNullException.ThrowIfNull(str);

            //adv trim
            str = str.Trim();
            str = str.Replace(" ", "");
            str = str.ToLower();

            List<string> result = [];

            while (true)
            {
                if (str.Length <= 0)
                    break;

                if (!str.Contains(argsPause))
                {
                    result.Add(str);
                    return result;
                }
                ;
                result.Add(str[0..(str.IndexOf(argsPause))]);
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