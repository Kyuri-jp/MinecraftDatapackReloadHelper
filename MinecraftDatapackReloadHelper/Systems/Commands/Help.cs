using MinecraftDatapackReloadHelper.Interfaces.Commands;

namespace MinecraftDatapackReloadHelper.Systems.Commands
{
    internal class Help : IToolCommand
    {
        public Task Run(Dictionary<string, List<string>> args)
        {
            Console.WriteLine("コマンドや引数は大文字小文字の区別はありません\n" +
                        "また、引数は -- で区切ります\n" +
                        "- は区切りとして認識されません\n" +
                        "引数についてはReadmeを参照ください -> https://github.com/Kyuri-jp/MinecraftDatapackReloadHelper\n");

            foreach (KeyValuePair<string, string> keyValuePair in Control.CommandSelector.GetCommandHelp())
                Console.WriteLine($"{keyValuePair.Key} : {keyValuePair.Value}");

            return Task.CompletedTask;
        }
    }
}