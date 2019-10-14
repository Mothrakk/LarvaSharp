using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LarvaSharp.LarvaLibs.Commanding;
using LarvaSharp.LarvaLibs.Managers;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    class AliveCheck : CommandInterface
    {
        public string HelpText()
        {
            return "Check if a given module's process is running.\nalive [modulename]";
        }

        public void Run(string[] args, ManagerInfo managerInfo = null)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Missing argument [modulename]");
            } else
            {
                if (managerInfo.ModuleManager.IsAvailableModule(args[0]))
                {
                    if (managerInfo.ModuleManager.ModuleMap[args[0]].ProcessManager.Alive()) {
                        Console.WriteLine("{0} is alive.", args[0]);
                    } else
                    {
                        Console.WriteLine("{0} is not alive.", args[0]);
                    }
                } else
                {
                    Console.WriteLine("Module {0} not found.", args[0]);
                }
            }
        }
    }
}
