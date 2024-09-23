using MinecraftDatapackReloadHelper.Libs.Github;

namespace MinecraftDatapackReloadHelper.Tools
{
    internal class UpdateCheck
    {
        internal static async Task UpdateCheckerAsync()
        {
            const string owner = "Kyuri-jp";
            const string repo = "MinecraftDatapackReloadHelper";
            string? latest = await GetLatestReleasetag.GetLatestReleaseTagAsync(owner, repo);
            Console.WriteLine($"Client : {Programs.GetAppVersion()}\n" +
                $"Latest : {latest}");
            Console.WriteLine($"https://github.com/{owner}/{repo}/releases/latest");
        }
    }
}