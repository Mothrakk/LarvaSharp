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
            ManagerCollection = new ManagerCollection(commandManager, new ModuleManager(Utility.RelativePath("modules")));
            ManagerCollection.CommandManager.ManagerCollection = ManagerCollection;
            ManagerCollection.CommandManager.Handle("greet");
            MainLoop();
        }

        /// <summary>
        /// The main loop of the class. Input gets handled, output gets printed.
        /// </summary>
        private void MainLoop()
        {
            string[] autoComplete;
            int i, j;
            List<string> words = new List<string>();
            ConsoleKeyInfo cki;

            while (Utility.Tick(100))
            {
                if (Console.KeyAvailable)
                {
                    autoComplete = null;
                    words.Clear();
                    j = 0;
                    cki = Console.ReadKey(true);

                    while (cki.Key != ConsoleKey.Enter)
                    {
                        if (cki.Key != ConsoleKey.Tab)
                        {
                            autoComplete = null;
                        }

                        if (cki.Key == ConsoleKey.Backspace)
                        {
                            if (words.Count > 0)
                            {
                                Console.Write('\r' + new string(' ', Console.WindowWidth - 1));
                                i = words.Count - 1;
                                if (words[i].Length > 0)
                                {
                                    words[i] = words[i].Substring(0, words[i].Length - 1);
                                } else
                                {
                                    words.RemoveAt(i);
                                }
                            }
                        } else if (cki.Key == ConsoleKey.Tab)
                        {
                            if (words.Count > 0)
                            {
                                i = words.Count - 1;
                                if (autoComplete == null)
                                {
                                    if (words.Count > 0)
                                    {
                                        autoComplete = ManagerCollection.Filter(words[i]);
                                        j = 0;
                                        if (autoComplete != null)
                                        {
                                            words[i] = autoComplete[j];
                                            j = (j + 1) % autoComplete.Length;
                                        }
                                    }
                                }
                                else
                                {
                                    words[i] = autoComplete[j];
                                    j = (j + 1) % autoComplete.Length;
                                }
                            }
                        } else if (cki.Key == ConsoleKey.Spacebar)
                        {
                            if (words.Count > 0 && words[words.Count - 1].Length > 0)
                            {
                                words.Add("");
                            }
                        } else
                        {
                            if (words.Count == 0)
                            {
                                words.Add(cki.KeyChar.ToString());
                            } else
                            {
                                words[words.Count - 1] += cki.KeyChar;
                            }
                        }

                        Console.Write(string.Format("\r>{0}", string.Join(" ", words)));
                        cki = Console.ReadKey(true);
                    }

                    Console.Write('\n');
                    HandleInput(string.Join(" ", words));
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