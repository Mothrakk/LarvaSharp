using LarvaSharp.LarvaLibs.Managers;
using System;
using System.IO;

namespace LarvaSharp.LarvaLibs
{
    /// <summary>
    /// The "brain" class of the project. <para/>
    /// Creating a new instance of this class also automatically starts <see cref="MainLoop"/>.
    /// </summary>
    internal class Larva
    {
        private ManagerCollection ManagerCollection { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Larva"/> class.
        /// </summary>
        internal Larva()
        {
            Console.Clear();

            foreach (string dirpath in new string[] { "modules", "pipeline" })
            {
                Directory.CreateDirectory(dirpath);
            }

            ManagerCollection = new ManagerCollection(new CommandManager(), new ModuleManager("modules"));
            ManagerCollection.CommandManager.ManagerCollection = ManagerCollection;
            ManagerCollection.CommandManager.Handle("logo");
            ManagerCollection.CommandManager.Handle("help");

            MainLoop();
        }

        /// <summary>
        /// The main loop of the class. Input gets handled, output gets printed.
        /// </summary>
        private void MainLoop()
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

        /// <summary>
        /// Handles the input.
        /// </summary>
        /// <param name="inp">The input.</param>
        private void HandleInput(string inp)
        {
            string[] inpSplit = inp.Split(' ');
            string first = inpSplit[0];
            string[] args = new string[inpSplit.Length - 1];

            for (int i = 1; i < inpSplit.Length; i++)
            {
                args[i - 1] = inpSplit[i];
            }

            if (ManagerCollection.CommandManager.IsCommand(first))
            {
                ManagerCollection.CommandManager.Handle(first, args);
            } else if (ManagerCollection.ModuleManager.IsAvailableModule(first)) {
                File.AppendAllText(Utility.Pipeline(first), string.Join(" ", args) + '\n');
            }
        }

        /// <summary>
        /// Reads the output.
        /// </summary>
        private void ReadOutput()
        {
            foreach (string line in Utility.FlushPipeline("larva"))
            {
                Console.WriteLine(line);
            }
        }
    }
}