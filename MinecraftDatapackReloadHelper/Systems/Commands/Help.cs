using MinecraftDatapackReloadHelper.Interfaces.Commands;
using MinecraftDatapackReloadHelper.Libs.String;
using MinecraftDatapackReloadHelper.Systems.Control;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Help : IToolCommand, IHasArgsCommand
    {
        private enum Args
        {
            More
        }

        private readonly Dictionary<string, string[]> _argsData = new()
        {
            {Args.More.ToString(),["指定したコマンドの詳細を表示します",$"--{Args.More}=[<Command>]"]}
        };

        Task IToolCommand.Run(Dictionary<string, List<string>> args)
        {
            if (args.ContainsKey(Args.More.ToString()))
            {
                if (args[Args.More.ToString()].Count <= 0 || string.IsNullOrEmpty(args[Args.More.ToString()][0]))
                {
                    Console.WriteLine("Please set any command.");
                    return Task.CompletedTask;
                }

                if (!CommandSelector.GetCommandInst().ContainsKey(Utils.ToUpperOnlyFirstLetter(args[Args.More.ToString()][0])))
                {
                    Console.WriteLine($"{args[Args.More.ToString()][0]} was not found.");
                    return Task.CompletedTask;
                }

                if (!CommandSelector.GetCommandInst()[Utils.ToUpperOnlyFirstLetter(args[Args.More.ToString()][0])].GetType().GetInterfaces().Contains(typeof(IHasArgsCommand)))
                {
                    Console.WriteLine($"Args of {args.ElementAt(0).Key} was not found.");
                    return Task.CompletedTask;
                }
                IHasArgsCommand command = (IHasArgsCommand)CommandSelector.GetCommandInst()[Utils.ToUpperOnlyFirstLetter(args[Args.More.ToString()][0])];
                Console.WriteLine($"[{Utils.ToUpperOnlyFirstLetter(args[Args.More.ToString()][0])}]-> {CommandSelector.GetCommandHelp()[Utils.ToUpperOnlyFirstLetter(args[Args.More.ToString()][0])]}");
                foreach (KeyValuePair<string, string[]> keyValuePair in command.GetArgs())
                    Console.WriteLine($"{keyValuePair.Key} : {string.Join(" / ", keyValuePair.Value)}");
            }
            else
            {
                Console.WriteLine("コマンドや引数は大文字小文字の区別はありません\n" +
                            "また、引数は -- で区切ります\n" +
                            "- は区切りとして認識されません\n" +
                            "引数についてはReadmeを参照ください -> https://github.com/Kyuri-jp/MinecraftDatapackReloadHelper\n" +
                            "help --more=[<command>] を使用するとより詳しい情報を見ることができます");

                foreach (KeyValuePair<string, string> keyValuePair in CommandSelector.GetCommandHelp())
                    Console.WriteLine($"{keyValuePair.Key} : {keyValuePair.Value}");
            }
            return Task.CompletedTask;
        }

        Dictionary<string, string[]> IHasArgsCommand.GetArgs() => _argsData;
    }
}