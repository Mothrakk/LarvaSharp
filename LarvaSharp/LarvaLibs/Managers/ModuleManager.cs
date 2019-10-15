using LarvaSharp.LarvaLibs.Modulation;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace LarvaSharp.LarvaLibs.Managers
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