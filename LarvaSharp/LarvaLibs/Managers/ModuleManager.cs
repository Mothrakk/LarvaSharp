﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        }

        public void AutoStart()
        {
            string[] lineSplit, args;
            string module;
            foreach (string line in Config.AutoStart.Read())
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

        public void RefreshModules(bool autostart = true)
        {
            if (Modules != null)
            {
                foreach (Module m in Modules)
                {
                    m.ProcessManager.Kill();
                }
            }

            string[] directories = Directory.GetDirectories(PathToModules)
                .Where(dir => !dir.EndsWith("__"))
                .Where(dir => !dir.StartsWith("."))
                .ToArray();
            Modules = new Module[directories.Length];
            ModuleMap = new Dictionary<string, Module>();

            for (int i = 0; i < directories.Length; i++)
            {
                Modules[i] = new Module(directories[i]);
                ModuleMap.Add(Modules[i].Name, Modules[i]);
            }

            if (autostart)
            {
                AutoStart();
            }
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