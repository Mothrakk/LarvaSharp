using LarvaSharp.LarvaLibs.Modulation;
using System;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    internal class AliveCheck : ICommandInterface
    {
        public string HelpText()
        {
            return "Check the process status of every available module or a specific module.\nalive [modulename]";
        }

        public void Run(string[] args, ManagerCollection managerCollection)
        {
            if (args.Length == 0)
            {
                foreach (string moduleName in managerCollection.ModuleManager.ModuleMap.Keys)
                {
                    managerCollection.CommandManager.Handle("alive", new string[] { moduleName });
                }
            }
            else
            {
                if (managerCollection.ModuleManager.IsAvailableModule(args[0]))
                {
                    if (managerCollection.ModuleManager.ModuleMap[args[0]].ProcessManager.Alive())
                    {
                        Console.WriteLine("{0} is alive.", args[0]);
                    }
                    else
                    {
                        Console.WriteLine("{0} is not alive.", args[0]);
                    }
                }
                else
                {
                    Console.WriteLine("Module {0} not found.", args[0]);
                }
            }
        }
    }
}