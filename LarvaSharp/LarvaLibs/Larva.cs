using System;
using System.IO;
using LarvaSharp.LarvaLibs.Managers;

namespace LarvaSharp.LarvaLibs
{
    /// <summary>
    /// The "brain" class of the project. <para/>
    /// Creating a new instance of this class also automatically starts <see cref="MainLoop"/>.
    /// </summary>
    class Larva
    {
        public ManagerInfo ManagerInfo { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Larva"/> class.
        /// </summary>
        public Larva()
        {
            Console.Clear();

            foreach (string dirpath in new string[] { "modules", "pipeline" })
            {
                Directory.CreateDirectory(dirpath);
            }
            
            ManagerInfo = new ManagerInfo(new CommandManager(), new ModuleManager("modules"), this);
            ManagerInfo.CommandManager.Handle("logo", null, ManagerInfo);
            ManagerInfo.CommandManager.Handle("help", new string[0], ManagerInfo);

            MainLoop();
        }

        /// <summary>
        /// The main loop of the class. Input gets handled, output gets printed.
        /// </summary>
        void MainLoop()
        {
            while (Utility.Tick(100))
            {
                if (Console.KeyAvailable)
                {
                    Console.Write('>');
                    HandleInput(Console.ReadLine());
                }
                ReadOutput();
            }
        }

        void HandleInput(string inp)
        {
            string[] inpSplit = inp.Split(' ');
            string first = inpSplit[0];
            string[] args = new string[inpSplit.Length - 1];

            for (int i = 1; i < inpSplit.Length; i++)
            {
                args[i - 1] = inpSplit[i];
            }

            if (ManagerInfo.CommandManager.IsCommand(first))
            {
                ManagerInfo.CommandManager.Handle(first, args, ManagerInfo);
            }
        }

        void ReadOutput()
        {
            foreach (string line in Utility.FlushPipeline("larva"))
            {
                Console.WriteLine(line);
            }
        }
    }
}
