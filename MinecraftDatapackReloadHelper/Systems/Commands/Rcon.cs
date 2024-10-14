using MinecraftDatapackReloadHelper.Abstract.Commands;
using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Libs.Console.Asker;
using MinecraftDatapackReloadHelper.Libs.Minecraft;
using MinecraftDatapackReloadHelper.Systems.Control;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Rcon : Command, IArgsable
    {
        private enum Args
        {
            Generatepassword,
            Connectiontest
        }

        private readonly Dictionary<string, string[]> _argsData = new()
        {
            { Args.Generatepassword.ToString(), ["パスワードをランダムに設定します", "--generatepassword"] },
            { Args.Connectiontest.ToString(), ["Rconの接続をテストします", "--connectiontest"] }
        };

        internal override async Task Run(Dictionary<string, List<string>> args)
        {
            if (args.ContainsKey(Args.Generatepassword.ToString()))
            {
                int range;
                while (true)
                {
                    string rangeS = Asker.Ask("Enter password range.");
                    if (int.TryParse(rangeS, out var result))
                    {
                        range = result;
                        break;
                    }

                    Console.WriteLine("Please enter int only.");
                }

                Dictionary<string, string> rconData = new Dictionary<string, string>()
                {
                    {"rcon.password",Libs.String.Utils.GenerateRandomString(range)}
                };

                Console.WriteLine($"Generated Password -> {rconData["rcon.password"]}");
                ServerProperties.Write(Path.Combine(Directory.GetParent(Settings.Copypath)!.Parent!.FullName,
                    "server.properties"), rconData);
                return;
            }

            if (args.ContainsKey(Args.Connectiontest.ToString()))
            {
                await ConnectionTest.ConnectingTesterAsync();
                return;
            }

            Console.WriteLine($"Please set any args. (--{Args.Connectiontest.ToString()},--{Args.Generatepassword.ToString()})");
        }

        Dictionary<string, string[]> IArgsable.GetArgs() => _argsData;
    }
}