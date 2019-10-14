using LarvaSharp.LarvaLibs.Managers;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    internal class Shell : CommandInterface
    {
        public Shell()
        {
        }

        public string HelpText()
        {
            return "Run the given arguments in the shell and print out the output.";
        }

        /// <summary>
        /// Run the given arguments in the shell and print out the output.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="managerCollection">Command manager info.</param>
        /// <returns></returns>
        public void Run(string[] args, ManagerCollection managerCollection = null)
        {
            string argsString = string.Format("/C {0}", string.Join(" ", args));
            Utility.RunProcess("cmd.exe", argsString);
        }
    }
}