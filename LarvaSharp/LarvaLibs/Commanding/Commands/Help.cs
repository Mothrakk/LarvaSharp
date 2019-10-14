using System;
using LarvaSharp.LarvaLibs.Commanding;
using LarvaSharp.LarvaLibs.Managers;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    class Help : CommandInterface
    {
        public Help() { }

        public string HelpText()
        {
            return "help [arg]: give more extensive documentation on a given command\nhelp: list out the available modules and hardcommands.";
        }

        /// <summary>
        /// Print out the available modules and hardcommands. <para/>
        /// 
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="managerInfo">Manager info.</param>
        public void Run(string[] args, ManagerInfo managerInfo = null)
        {
            if (args.Length > 0)
            {
                if (!managerInfo.CommandMap.ContainsKey(args[0]))
                {
                    Console.WriteLine("{0} not found", args[0]);
                }
                else
                {
                    Console.WriteLine("{0}: {1}", args[0], managerInfo.CommandMap[args[0]].HelpText());
                }
            }
            else
            {
                Console.WriteLine("cmds: " + string.Join("; ", managerInfo.CommandMap.Keys));
                Console.WriteLine("modules: " + managerInfo.ModuleManager.ToString());
            }
        }
    }
}
