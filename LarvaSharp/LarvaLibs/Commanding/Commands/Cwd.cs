using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LarvaSharp.LarvaLibs.Modulation;
using LarvaSharp.LarvaLibs;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    class Cwd : ICommandInterface
    {
        public string HelpText()
        {
            return "Open the current working directory of the executable.";
        }

        public void Run(string[] args, ManagerCollection managerCollection)
        {
            Utility.RunProcess("explorer.exe", Utility.RelativePath(""));
        }
    }
}
