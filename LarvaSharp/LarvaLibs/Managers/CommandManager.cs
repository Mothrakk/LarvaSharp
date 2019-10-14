using System.Collections.Generic;
using LarvaSharp.LarvaLibs.Commanding.Commands;
using LarvaSharp.LarvaLibs.Commanding;

namespace LarvaSharp.LarvaLibs.Managers
{
    class CommandManager
    {
        public Dictionary<string, CommandInterface> CommandMap { get; }

        public CommandManager()
        {
            CommandMap = new Dictionary<string, CommandInterface>
            {
                { "shell", new Shell() },
                { "help" , new Help()  },
                { "cls"  , new Clear() },
                { "greet", new Greet() },
                { "logo" , new Logo() },
                { "eval" , new PythonEvaluate() },
                { "start", new Start() },
                { "alive", new AliveCheck() }
            };
        }

        public void Handle(string cmd, string[] args, ManagerInfo managerInfo)
        {
            CommandMap[cmd].Run(args, managerInfo);
        }

        public bool IsCommand(string s)
        {
            return CommandMap.ContainsKey(s);
        }
    }
}
