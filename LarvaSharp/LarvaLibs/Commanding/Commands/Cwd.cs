using LarvaSharp.LarvaLibs.Modulation;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    internal class Cwd : ICommandInterface
    {
        public string HelpText()
        {
            return "Open the current working directory of the executable.";
        }

        public void Run(string[] args, ManagerCollection managerCollection)
        {
            Utility.RunProcess("explorer.exe", Utility.RelativePath(""));
        }
    }
}