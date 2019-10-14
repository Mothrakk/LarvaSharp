using LarvaSharp.LarvaLibs.Managers;
using System;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    internal class Help : CommandInterface
    {
        public Help()
        {
        }

        public string HelpText()
        {
            return "help [arg]: give more extensive documentation on a given command\nhelp: list out the available modules and hardcommands.";
        }

        /// <summary>
        /// Print out the available modules and hardcommands. <para/>
        ///
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="managerCollection">Manager info.</param>
        public void Run(string[] args, ManagerCollection managerCollection = null)
        {
            if (args.Length > 0)
            {
                string cmd = args[0];
                if (!managerCollection.CommandManager.IsCommand(cmd))
                {
                    Console.WriteLine("{0} not found", cmd);
                }
                else
                {
                    Console.WriteLine("{0}: {1}", cmd, managerCollection.CommandManager.GetCommandHelpText(cmd));
                }
            }
            else
            {
                Console.WriteLine("cmds: " + managerCollection.CommandManager.ToString());
                Console.WriteLine("modules: " + managerCollection.ModuleManager.ToString());
            }
        }
    }
}