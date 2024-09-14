namespace MinecraftDatapackReloadHelper.Tools
{
    internal class StringUtl
    {
        internal static string ToUpperOnlyFirstLetter(string value)
        {
            char[] chars = value.ToLower().ToCharArray();
            chars[0] = char.ToUpper(chars[0]);
            return string.Join("", chars);
        }
    }
}