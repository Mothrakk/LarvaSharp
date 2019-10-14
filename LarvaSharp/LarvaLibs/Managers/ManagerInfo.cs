using System.Collections.Generic;
using LarvaSharp.LarvaLibs.Commanding;

namespace LarvaSharp.LarvaLibs.Managers
{
    class ManagerInfo
    {
        public CommandManager CommandManager { get; }
        public Dictionary<string, CommandInterface> CommandMap
        {
            get
            {
                return CommandManager.CommandMap;
            }
        }
        public ModuleManager ModuleManager { get; }
        public Larva Larva { get; }

        public ManagerInfo(CommandManager commandManager, ModuleManager moduleManager, Larva larva)
        {
            CommandManager = commandManager;
            ModuleManager = moduleManager;
            Larva = larva;
        }
    }
}
