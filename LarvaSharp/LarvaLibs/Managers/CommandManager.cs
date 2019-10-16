using LarvaSharp.LarvaLibs.Commanding;
using LarvaSharp.LarvaLibs.Commanding.Commands;
using System.Collections.Generic;
using System.Linq;

namespace LarvaSharp.LarvaLibs.Modulation
{
    internal class CommandManager
    {
        public ManagerCollection ManagerCollection { private get; set; }
        private Dictionary<string, ICommandInterface> CommandMap { get; }

        private string[] Commands
        {
            get
            {
                return CommandMap.Keys.ToArray();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManager"/> class.
        /// </summary>
        public CommandManager()
        {
            CommandMap = new Dictionary<string, ICommandInterface>
            {
                { "shell",   new Shell() },
                { "help" ,   new Help()  },
                { "cls"  ,   new Clear() },
                { "greet",   new Greet() },
                { "logo" ,   new Logo() },
                { "eval" ,   new PythonEvaluate() },
                { "start",   new Start() },
                { "kill" ,   new Kill() },
                { "alive",   new AliveCheck() },
                { "refresh", new Refresh() }
            };
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="args">The arguments.</param>
        public void Handle(string cmd, string[] args)
        {
            CommandMap[cmd].Run(args, ManagerCollection);
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        public void Handle(string cmd)
        {
            CommandMap[cmd].Run(new string[0], ManagerCollection);
        }

        /// <summary>
        /// Determines whether the specified command exists.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns>
        ///   <c>true</c> if the specified command exists; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCommand(string cmd)
        {
            return Commands.Contains(cmd);
        }

        /// <summary>
        /// Gets the command help text. Assumes that caller already checked if cmd exists.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns>Help text.</returns>
        public string GetCommandHelpText(string cmd)
        {
            return CommandMap[cmd].HelpText();
        }

        public override string ToString()
        {
            return string.Join("; ", Commands);
        }
    }
}