using LarvaSharp.LarvaLibs.Modulation;
using System;
using System.IO;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    internal class Help : ICommandInterface
    {
        public Help()
        {
        }

        public string HelpText()
        {
            return "Display more extensive documentation.\nhelp [void]: display all available commands and modules.\nhelp [arg]: display documentation about specified cmd/module";
        }

        /// <summary>
        /// Print out the available modules and hardcommands. <para/>
        ///
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="managerCollection">Manager info.</param>
        public void Run(string[] args, ManagerCollection managerCollection)
        {
            if (args.Length > 0)
            {
                string first = args[0];
                if (managerCollection.CommandManager.IsCommand(first))
                {
                    Console.WriteLine("{0}: {1}", first, managerCollection.CommandManager.GetCommandHelpText(first));
                }
                else if (managerCollection.ModuleManager.IsAvailableModule(first))
                {
                    string p = managerCollection.ModuleManager.ModuleMap[first].ModulePath + "\\help.larva";
                    if (File.Exists(p))
                    {
                        Console.WriteLine("{0}: {1}", first, File.ReadAllText(p));
                    }
                    else
                    {
                        Console.WriteLine("{0} has no help.larva file.", first);
                    }
                }
                else
                {
                    Console.WriteLine("{0} not found", first);
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