using LarvaSharp.LarvaLibs.Modulation;
using System;
using System.Linq;
using System.Text;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    internal class AutoStart : ICommandInterface
    {
        public string HelpText()
        {
            return "Flip the autostart option on given module.\nautostart [modulename]\nautostart [modulename] [autostart arg] [autostart arg]";
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
                    if (Config.AutoStart.Contains(moduleName))
                    {
                        Config.AutoStart.Erase(moduleName);
                        Console.WriteLine("{0} autostart set to off", moduleName);
                    }
                    else
                    {
                        StringBuilder o = new StringBuilder(moduleName);
                        if (args.Length > 1)
                        {
                            o.Append(':');
                            o.Append(string.Join(" ", args.ToList().GetRange(1, args.Length - 1)));
                        }
                        Config.AutoStart.Append(o.ToString());
                        Console.WriteLine("{0} autostart set to on", moduleName);
                    }
                }
            }
        }
    }
}