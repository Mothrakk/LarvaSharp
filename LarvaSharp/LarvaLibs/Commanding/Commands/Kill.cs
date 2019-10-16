using LarvaSharp.LarvaLibs.Modulation;
using System;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    internal class Kill : ICommandInterface
    {
        public string HelpText()
        {
            return "Attempt to kill selected module, if it is alive.\nkill [modulename]";
        }

        public void Run(string[] args, ManagerCollection managerCollection)
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
                    managerCollection.ModuleManager.ModuleMap[moduleName].ProcessManager.Kill();
                }
                else
                {
                    Console.WriteLine("Module {0} not found.", moduleName);
                }
            }
        }
    }
}