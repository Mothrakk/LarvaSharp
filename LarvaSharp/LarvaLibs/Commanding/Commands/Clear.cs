using System;
using LarvaSharp.LarvaLibs.Managers;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    class Clear : CommandInterface
    {
        public Clear() { }

        public string HelpText()
        {
            return "Clear the console.";
        }

        public void Run(string[] args, ManagerInfo managerInfo = null)
        {
            Console.Clear();
        }
    }
}
