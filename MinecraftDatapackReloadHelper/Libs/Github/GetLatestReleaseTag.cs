﻿using System.Text.Json;

//このプログラムはChatGPTによって生成されたものを参考に書かれています

namespace MinecraftDatapackReloadHelper.Libs.Github
{
    internal class GetLatestReleasetag
    {
        private static readonly HttpClient Client = new();

        public static async Task<string?> GetLatestReleaseTagAsync(string owner, string repo)
        {
            try
            {
                // GitHub Libsのエンドポイント
                string url = $"https://api.github.com/repos/{owner}/{repo}/releases/latest";

                // GitHub LibsはUser-Agentを要求するので設定
                Client.DefaultRequestHeaders.UserAgent.ParseAdd("Minecraft-Datapack-Reload-Helper");

                // GETリクエストの送信
                HttpResponseMessage response = await Client.GetAsync(url);
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
                System.Console.WriteLine("\nException Caught!");
                System.Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
    }
}