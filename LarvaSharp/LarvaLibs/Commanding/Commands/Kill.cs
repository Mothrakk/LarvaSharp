using LarvaSharp.LarvaLibs.Managers;
using System;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    internal class Kill : CommandInterface
    {
        public string HelpText()
        {
            return "Attempt to kill selected module, if it is alive.\nkill [modulename]";
        }

        public void Run(string[] args, ManagerCollection managerCollection = null)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Missing argument [modulename]");
            }
            else
            {
                string moduleName = args[0];
                if (managerCollection.ModuleManager.IsAvailableModule(moduleName))
                {
                    managerCollection.ModuleManager.ModuleMap[moduleName].Kill();
                }
                else
                {
                    Console.WriteLine("Module {0} not found.", moduleName);
                }
            }
        }
    }
}