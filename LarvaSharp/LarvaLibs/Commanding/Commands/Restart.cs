using LarvaSharp.LarvaLibs.Modulation;
using System;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    internal class Restart : ICommandInterface
    {
        public string HelpText()
        {
            return "Restart given module.\nrestart [modulename]";
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
                if (!managerCollection.ModuleManager.IsAvailableModule(moduleName))
                {
                    Console.WriteLine("{0} not found.", moduleName);
                }
                else
                {
                    foreach (string c in new string[] { "kill", "start" })
                    {
                        managerCollection.CommandManager.Handle(c, args);
                    }
                }
            }
        }
    }
}