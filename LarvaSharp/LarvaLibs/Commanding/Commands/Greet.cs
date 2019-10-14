using LarvaSharp.LarvaLibs.Commanding;
using LarvaSharp.LarvaLibs.Managers;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    class Greet : CommandInterface
    {
        public string HelpText()
        {
            return "Short for cls -> logo -> help.";
        }

        public void Run(string[] args, ManagerInfo managerInfo = null)
        {
            foreach (string c in new string[] { "cls", "logo", "help" })
            {
                managerInfo.CommandMap[c].Run(new string[0], managerInfo);
            }
        }
    }
}
