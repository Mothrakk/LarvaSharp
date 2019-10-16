using LarvaSharp.LarvaLibs.Modulation;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace LarvaSharp.LarvaLibs.Modulation
{
    internal class ModuleManager
    {
        private string PathToModules { get; }
        private Module[] Modules { get; set; }
        public Dictionary<string, Module> ModuleMap { get; private set; }

        public ModuleManager(string pathToModules)
        {
            PathToModules = pathToModules;
            RefreshModules();
        }

        private void AutoStart()
        {
            string p = Utility.Pipeline("autostart", ".larva");
            if (File.Exists(p))
            {
                string[] lineSplit, args;
                string module;
                foreach (string line in File.ReadAllLines(p))
                {
                    Console.Write("Autostart: ");
                    lineSplit = line.Split(':');
                    module = lineSplit[0];

                    if (IsAvailableModule(module))
                    {
                        if (lineSplit.Length == 2)
                        {
                            args = lineSplit[1].Split(' ');
                        }
                        else
                        {
                            args = new string[0];
                        }

                        ModuleMap[module].ProcessManager.Start(args);
                    }
                    else
                    {
                        Console.WriteLine("Module {0} is not available.", module);
                    }
                }
            }
        }

        public void RefreshModules()
        {
            string[] directories = Directory.GetDirectories(PathToModules).Where(dir => !dir.EndsWith("__")).ToArray();
            Modules = new Module[directories.Length];
            ModuleMap = new Dictionary<string, Module>();

            for (int i = 0; i < directories.Length; i++)
            {
                Modules[i] = new Module(directories[i]);
                ModuleMap.Add(Modules[i].Name, Modules[i]);
            }

            AutoStart();
        }

        public bool IsAvailableModule(string moduleName)
        {
            return ModuleMap.ContainsKey(moduleName);
        }

        public void Start(string modulename, string[] args)
        {
            ModuleMap[modulename].ProcessManager.Start(args);
        }

        public override string ToString()
        {
            return string.Join("; ", ModuleMap.Keys);
        }
    }
}