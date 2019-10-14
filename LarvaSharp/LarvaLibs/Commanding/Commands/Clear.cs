using LarvaSharp.LarvaLibs.Managers;
using System;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    internal class Clear : CommandInterface
    {
        public Clear()
        {
        }

        public string HelpText()
        {
            return "Clear the console.";
        }

        public void Run(string[] args, ManagerCollection managerCollection = null)
        {
            Console.Clear();
        }
    }
}