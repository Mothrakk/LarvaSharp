using LarvaSharp.LarvaLibs.Managers;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    internal class PythonEvaluate : CommandInterface
    {
        public string HelpText()
        {
            return "Pass the command into Python and print the output.";
        }

        public void Run(string[] args, ManagerCollection managerCollection = null)
        {
            string c = string.Format("-c print({0})", string.Join(" ", args));
            Utility.RunProcess("python.exe", c);
        }
    }
}