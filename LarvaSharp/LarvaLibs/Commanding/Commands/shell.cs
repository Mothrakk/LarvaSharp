using System;
using LarvaSharp.LarvaLibs.Commanding;
using LarvaSharp.LarvaLibs.Managers;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    class Shell : CommandInterface
    {
        public Shell() { }

        public string HelpText()
        {
            return "Run the given arguments in the shell and print out the output.";
        }

        /// <summary>
        /// Run the given arguments in the shell and print out the output.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="managerInfo">Command manager info.</param>
        /// <returns></returns>
        public void Run(string[] args, ManagerInfo managerInfo = null)
        {
            string argsString = string.Format("/C {0}", string.Join(" ", args));
            Utility.RunProcess("cmd.exe", argsString);
        }
    }
}
