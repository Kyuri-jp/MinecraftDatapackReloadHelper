namespace MinecraftDatapackReloadHelper.Libs.Minecraft
{
    internal class ServerProperties
    {
        internal static Dictionary<string, string> Parse(string file)
        {
            Dictionary<string, string> value = [];
            string[] fileData = File.ReadAllLines(file);
            foreach (var item in fileData)
            {
                if (item[0] == '#') continue;
                value.Add(item[..item.IndexOf('=')], item[(item.IndexOf('=') + 1)..]);
            }
            return value;
        }
    }
}