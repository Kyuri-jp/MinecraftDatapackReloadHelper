namespace MinecraftDatapackReloadHelper.Tools.Minecraft
{
    internal class ServerProperties
    {
        internal static Dictionary<string, string> Parse(string file)
        {
            Dictionary<string, string> value = [];
            string[] fileData = File.ReadAllLines(file);
            foreach (var item in fileData)
                value.Add(item[..item.IndexOf('=')], item[(item.IndexOf('=') + 1)..]);

            //ShowAnalyzeData(value);
            return value;
        }

        private static void ShowAnalyzeData(Dictionary<string, string> data)
        {
            foreach (KeyValuePair<string, string> keyValuePair in data)
            {
                Console.WriteLine($"{keyValuePair.Key} : {keyValuePair.Value}");
            }
        }
    }
}
