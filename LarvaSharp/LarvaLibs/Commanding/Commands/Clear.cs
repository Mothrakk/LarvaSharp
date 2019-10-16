using LarvaSharp.LarvaLibs.Modulation;
using System;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    internal class Clear : ICommandInterface
    {
        public Clear()
        {
        }

        public string HelpText()
        {
            return "Clear the console.";
        }

        public void Run(string[] args, ManagerCollection managerCollection)
        {
            Console.Clear();
        }
    }
}