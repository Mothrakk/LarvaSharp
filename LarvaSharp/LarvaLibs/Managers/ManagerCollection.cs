using System.Collections.Generic;
using System.Linq;

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
            commandManager.ManagerCollection = this;
        }

        public string[] Filter(string mustStartWith)
        {
            return CommandManager.CommandMap.Keys.Union(ModuleManager.ModuleMap.Keys)
                .Where(key => key.StartsWith(mustStartWith))
                .ToArray();
        }
    }
}