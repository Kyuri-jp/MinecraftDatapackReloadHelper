using System.Diagnostics;

namespace MinecraftDatapackReloadHelper.Libs.Console
{
    internal static class Dos
    {
        //ref https://resanaplaza.com/2021/05/03/%e3%80%90%e3%82%b3%e3%83%94%e3%83%9a%e3%81%a7%e5%ae%8c%e4%ba%86%e3%80%91c%e3%81%a7dos-%e3%82%b3%e3%83%9e%e3%83%b3%e3%83%89%e3%82%92%e5%ae%9f%e8%a1%8c%e3%81%97%e7%b5%90%e6%9e%9c%e3%82%92%e5%8f%96/
        internal static IEnumerable<string> RunCommand(string command)
        {
            ProcessStartInfo psInfo = new ProcessStartInfo
            {
                FileName = "cmd", // 実行するファイル
                Arguments = "/c " + command,//引数
                //CreateNoWindow = true, // コンソール・ウィンドウを開かない
                UseShellExecute = false, // シェル機能を使用しない
                RedirectStandardOutput = true // 標準出力をリダイレクト
            };

            Process p = Process.Start(psInfo)!; // アプリの実行開始

            while (p.StandardOutput.ReadLine() is { } line)
            {
                yield return line;
            }
        }

        internal static void ShowResult(this IEnumerable<string> iEnumerable)
        {
            foreach (string item in iEnumerable)
                System.Console.WriteLine(item);
        }
    }
}