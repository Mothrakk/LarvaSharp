using LarvaSharp.LarvaLibs.Managers;
using System;
using System.IO;
using System.Linq;

namespace LarvaSharp.LarvaLibs.Modulation
{
    internal class Module
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
                Console.WriteLine("Module {0} err: either couldn't find an executable ({0}.py || {0}.exe) or found too many.", Name);
                Filename = null;
                Extension = null;
                ExecutablePath = null;
            }
            else
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

        public void Kill()
        {
            ProcessManager.Kill();
        }

        public bool Healthy()
        {
            return ExecutablePath != null;
        }
    }
}