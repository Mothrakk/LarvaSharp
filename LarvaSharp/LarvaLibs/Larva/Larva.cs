using LarvaSharp.LarvaLibs.Modulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

            foreach (string p in new string[] { "modules", "pipeline", "logos" })
            {
                Directory.CreateDirectory(Utility.RelativePath(p));
            }

            CommandManager commandManager = new CommandManager();
            commandManager.Handle("logo");
            ManagerCollection = new ManagerCollection(commandManager, new ModuleManager(Utility.RelativePath("modules")));
            ManagerCollection.CommandManager.ManagerCollection = ManagerCollection;
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
            List<string> inpSplit = inp.Split(' ').ToList();
            string first = inpSplit[0];
            string[] args = inpSplit
                .GetRange(1, inpSplit.Count - 1)
                .ToArray();

            if (ManagerCollection.CommandManager.IsCommand(first))
            {
                ManagerCollection.CommandManager.Handle(first, args);
            }
            else if (ManagerCollection.ModuleManager.IsAvailableModule(first))
            {
                File.AppendAllText(Utility.Pipeline(first), string.Join(" ", args) + '\n');
            }
            else
            {
                Console.WriteLine("Unknown input: " + first);
            }
        }

        /// <summary>
        /// Reads the output.
        /// </summary>
        private void ReadOutput()
        {
            bool done = false;
            while (!done)
            {
                try
                {
                    foreach (string line in Utility.FlushPipeline("larva"))
                    {
                        Console.WriteLine(line);
                    }
                    done = true;
                }
                catch (IOException) { }
            }
        }
    }
}