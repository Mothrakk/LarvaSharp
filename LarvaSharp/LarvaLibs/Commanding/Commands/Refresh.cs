using LarvaSharp.LarvaLibs.Modulation;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    internal class Refresh : ICommandInterface
    {
        public string HelpText()
        {
            return "Refresh and re-initialize the modules.";
        }

        public void Run(string[] args, ManagerCollection managerCollection)
        {
            managerCollection.ModuleManager.RefreshModules();
        }
    }
}