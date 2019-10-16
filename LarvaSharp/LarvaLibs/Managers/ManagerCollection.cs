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
    }
}