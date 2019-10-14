using System.IO;
using System.Linq;
using System.Collections.Generic;
using LarvaSharp.LarvaLibs.Modulation;

namespace LarvaSharp.LarvaLibs.Managers
{
    class ModuleManager
    {
        readonly string pathToModules;
        Module[] modules;
        public Dictionary<string, Module> ModuleMap { get; private set; }

        public ModuleManager(string pathToModules)
        {
            this.pathToModules = pathToModules;
            RefreshModules();
        }

        public void RefreshModules()
        {
            string[] directories = Directory.GetDirectories(pathToModules);
            modules = new Module[directories.Length];
            ModuleMap = new Dictionary<string, Module>();

            for (int i = 0; i < directories.Length; i++)
            {
                modules[i] = new Module(directories[i]);
                ModuleMap.Add(modules[i].Name, modules[i]);
            }
        }

        public bool IsAvailableModule(string moduleName)
        {
            return ModuleMap.ContainsKey(moduleName);
        }

        public void Start(string modulename, string[] args)
        {
            ModuleMap[modulename].Start(args);
        }

        public override string ToString()
        {
            return string.Join("; ", ModuleMap.Keys);
        }
    }
}
