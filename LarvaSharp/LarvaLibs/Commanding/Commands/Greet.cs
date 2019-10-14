using LarvaSharp.LarvaLibs.Managers;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    internal class Greet : CommandInterface
    {
        public string HelpText()
        {
            return "Short for cls -> logo -> help.";
        }

        public void Run(string[] args, ManagerCollection managerCollection = null)
        {
            foreach (string c in new string[] { "cls", "logo", "help" })
            {
                managerCollection.CommandManager.Handle(c);
            }
        }
    }
}