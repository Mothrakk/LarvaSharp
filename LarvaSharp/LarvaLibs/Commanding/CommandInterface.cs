using LarvaSharp.LarvaLibs.Managers;

namespace LarvaSharp.LarvaLibs.Commanding
{
    internal interface CommandInterface
    {
        void Run(string[] args, ManagerCollection managerCollection = null);

        string HelpText();
    }
}