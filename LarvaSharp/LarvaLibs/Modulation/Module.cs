using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Diagnostics;
using LarvaSharp.LarvaLibs.Managers;

namespace LarvaSharp.LarvaLibs.Modulation
{
    class Module
    {
        public string ModulePath { get; }
        public string CFGPath { get; }
        public string ExecutablePath { get; }

        public string Name { get; }
        public string Extension { get; }
        public string Filename { get; }

        public ProcessManager ProcessManager { get; }
        
        public Module(string pathToModule)
        {
            ModulePath = pathToModule;
            CFGPath = pathToModule + "\\cfg.txt";

            string[] pathSplit = ModulePath.Split('\\');
            Name = pathSplit[pathSplit.Length - 1];

            string[] executables = Directory.EnumerateFiles(ModulePath)
                .Where(f => f.Split('\\').Last().StartsWith(Name) && (f.EndsWith(".py") || f.EndsWith(".exe")))
                .ToArray();
            
            if (executables.Length != 1)
            {
                Console.WriteLine("Module {0} err: either couldn't find an executable or found too many.", Name);
                Filename = null;
                Extension = null;
                ExecutablePath = null;
            } else
            {
                ExecutablePath = executables[0];
                Filename = ExecutablePath.Split('\\').Last();
                Extension = Filename.Split('.')[1];
            }

            ProcessManager = new ProcessManager(this);
        }

        public void Start(string[] args)
        {
            ProcessManager.Start(args);
        }

        public bool Healthy()
        {
            return ExecutablePath != null;
        }
    }
}
