using System.Collections.Generic;

namespace LarvaSharp.LarvaLibs.Modulation
{
    internal class ManagerCollection
    {
        public CommandManager CommandManager { get; }
        public ModuleManager ModuleManager { get; }

        public ManagerCollection(CommandManager commandManager, ModuleManager moduleManager)
        {
            CommandManager = commandManager;
            ModuleManager = moduleManager;
        }

        public string[] Filter(string mustStartWith)
        {
            List<string> filtered = new List<string>();

            foreach (string s in CommandManager.CommandMap.Keys)
            {
                if (s.StartsWith(mustStartWith))
                {
                    filtered.Add(s);
                }
            }

            foreach (string s in ModuleManager.ModuleMap.Keys)
            {
                if (s.StartsWith(mustStartWith))
                {
                    filtered.Add(s);
                }
            }

            if (filtered.Count > 0)
            {
                return filtered.ToArray();
            }
            return null;
        }
    }
}