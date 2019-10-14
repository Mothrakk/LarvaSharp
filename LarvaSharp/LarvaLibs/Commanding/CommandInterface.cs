using LarvaSharp.LarvaLibs.Managers;

namespace LarvaSharp.LarvaLibs.Commanding
{
    internal interface ICommandInterface
    {
        void Run(string[] args, ManagerCollection managerCollection);

        string HelpText();
    }
}