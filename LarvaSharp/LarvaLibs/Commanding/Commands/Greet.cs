using LarvaSharp.LarvaLibs.Modulation;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    internal class Greet : ICommandInterface
    {
        public string HelpText()
        {
            return "Short for cls -> logo -> help.";
        }

        public void Run(string[] args, ManagerCollection managerCollection)
        {
            foreach (string c in new string[] { "cls", "logo", "help" })
            {
                managerCollection.CommandManager.Handle(c);
            }
        }
    }
}