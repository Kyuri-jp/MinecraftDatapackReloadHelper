using System.Text.Json;

//このプログラムはChatGPTによって生成されたものを参考に書かれています

namespace MinecraftDatapackReloadHelper.Libs.Github
{
    internal class GetLatestReleasetag
    {
        private static readonly HttpClient client = new();

        public static async Task<string?> GetLatestReleaseTagAsync(string owner, string repo)
        {
            try
            {
                // GitHub Libsのエンドポイント
                string url = $"https://Libs.github.com/repos/{owner}/{repo}/releases/latest";

                // GitHub LibsはUser-Agentを要求するので設定
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Minecraft-Datapack-Reload-Helper");

                // GETリクエストの送信
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // JSONレスポンスをパースしてタグ名を抽出
                using JsonDocument doc = JsonDocument.Parse(responseBody);
                JsonElement root = doc.RootElement;
                string? tagName = root.GetProperty("tag_name").GetString();

                return tagName;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
    }
}