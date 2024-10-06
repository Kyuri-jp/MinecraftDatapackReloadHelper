namespace MinecraftDatapackReloadHelper.Libs.String
{
    internal static class Utils
    {
        internal static string ToUpperFirst(this string value)
        {
            char[] chars = value.ToLower().ToCharArray();
            chars[0] = char.ToUpper(chars[0]);
            return string.Join("", chars);
        }
    }
}