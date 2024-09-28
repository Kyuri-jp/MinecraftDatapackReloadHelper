using MinecraftDatapackReloadHelper.Libs.Files;

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
                value.Add(item[..item.IndexOf('=')].Trim(), item[(item.IndexOf('=') + 1)..].Trim());
            }
            return value;
        }

        internal static void Write(string file, Dictionary<string, string> value)
        {
            Dictionary<string, string> fileData = Parse(file);
            foreach (var item in value.Where(item => fileData.ContainsKey(item.Key)))
                fileData[item.Key] = item.Value;

            List<string> writeList = [];
            writeList.AddRange(fileData.Select(item => $"{item.Key}={item.Value}"));
            File.WriteAllLines(file, writeList, Encode.GetEncoding(file));
        }
    }
}