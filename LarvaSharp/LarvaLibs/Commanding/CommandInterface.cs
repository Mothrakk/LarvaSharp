using LarvaSharp.LarvaLibs.Managers;

namespace LarvaSharp.LarvaLibs.Commanding
{
    interface CommandInterface
    {
        void Run(string[] args, ManagerInfo managerInfo = null);

        string HelpText();
    }
}
