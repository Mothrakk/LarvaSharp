using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LarvaSharp.LarvaLibs.Modulation;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    class Refresh : ICommandInterface
    {
        public string HelpText()
        {
            return "Refresh and re-initialize the modules.";
        }

        public void Run(string[] args, ManagerCollection managerCollection)
        {
            managerCollection.ModuleManager.RefreshModules();
        }
    }
}
