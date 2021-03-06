﻿using LarvaSharp.LarvaLibs.Modulation;
using System;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    internal class Start : ICommandInterface
    {
        public string HelpText()
        {
            return "Attempt to start a module.\nstart [modulename] [arg] [arg]...";
        }

        public void Run(string[] args, ManagerCollection managerCollection)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Lacking argument: [modulename]");
            }
            else
            {
                string moduleName = args[0];
                if (managerCollection.ModuleManager.IsAvailableModule(moduleName))
                {
                    string[] realArgs = new string[args.Length - 1];
                    for (int i = 1; i < args.Length; i++)
                    {
                        realArgs[i - 1] = args[i];
                    }
                    managerCollection.ModuleManager.Start(moduleName, realArgs);
                }
                else
                {
                    Console.WriteLine("{0} not found.", moduleName);
                }
            }
        }
    }
}