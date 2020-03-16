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
            foreach (string p in new string[] { "modules", "pipeline", "logos", "config" })
            {
                Directory.CreateDirectory(Utility.RelativePath(p));
            }

            ManagerCollection = new ManagerCollection(new CommandManager(), new ModuleManager(Utility.RelativePath("modules")));
            ManagerCollection.CommandManager.ManagerCollection = ManagerCollection;

            ManagerCollection.ModuleManager.RefreshModules(false);
            ManagerCollection.CommandManager.Handle("greet");
            ManagerCollection.ModuleManager.AutoStart();

            MainLoop();
        }

        /// <summary>
        /// The main loop of the class. Input gets handled, output gets printed.
        /// </summary>
        private void MainLoop()
        {
            string[] autoComplete;
            int i, autoCompleteIDX, historyIDX;
            List<string> words = new List<string>();
            List<string> history = new List<string>();
            ConsoleKeyInfo cki;

            while (Utility.Tick(100))
            {
                if (Console.KeyAvailable)
                {
                    autoComplete = null;
                    words.Clear();
                    autoCompleteIDX = 0;
                    historyIDX = history.Count - 1;
                    cki = Console.ReadKey(true);

                    while (cki.Key != ConsoleKey.Enter)
                    {
                        if (cki.Key != ConsoleKey.Tab)
                        {
                            autoComplete = null;
                        }

                        if (cki.Key == ConsoleKey.Backspace)
                        {
                            Console.Write('\r' + new string(' ', Console.WindowWidth - 1));
                            if (words.Count > 0)
                            {
                                i = words.Count - 1;
                                if (words[i].Length > 0)
                                {
                                    words[i] = words[i].Substring(0, words[i].Length - 1);
                                }
                                else
                                {
                                    words.RemoveAt(i);
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        else if (cki.Key == ConsoleKey.UpArrow || cki.Key == ConsoleKey.DownArrow)
                        {
                            if (historyIDX != -1)
                            {
                                Console.Write('\r' + new string(' ', Console.WindowWidth - 1));
                                words = history[historyIDX].Split(' ').ToList();
                                int shift = (cki.Key == ConsoleKey.UpArrow) ? -1 : 1;
                                historyIDX = (historyIDX + shift) % history.Count;
                                if (historyIDX == -1)
                                {
                                    historyIDX = history.Count - 1;
                                }
                            }
                        }
                        else if (cki.Key == ConsoleKey.Tab)
                        {
                            if (words.Count > 0)
                            {
                                i = words.Count - 1;
                                if (autoComplete == null)
                                {
                                    if (words.Count > 0)
                                    {
                                        autoComplete = ManagerCollection.Filter(words[i]);
                                        autoCompleteIDX = 0;
                                        if (autoComplete != null)
                                        {
                                            words[i] = autoComplete[autoCompleteIDX];
                                            autoCompleteIDX = (autoCompleteIDX + 1) % autoComplete.Length;
                                        }
                                    }
                                }
                                else
                                {
                                    words[i] = autoComplete[autoCompleteIDX];
                                    autoCompleteIDX = (autoCompleteIDX + 1) % autoComplete.Length;
                                }
                            }
                        }
                        else if (cki.Key == ConsoleKey.Spacebar)
                        {
                            if (words.Count > 0 && words[words.Count - 1].Length > 0)
                            {
                                words.Add("");
                            }
                        }
                        else
                        {
                            if (words.Count == 0)
                            {
                                words.Add(cki.KeyChar.ToString());
                            }
                            else
                            {
                                words[words.Count - 1] += cki.KeyChar;
                            }
                        }

                        Console.Write(string.Format("\r>{0}", string.Join(" ", words)));
                        cki = Console.ReadKey(true);
                    }

                    if (words.Count > 0)
                    {
                        Console.Write('\n');
                        string done = string.Join(" ", words);
                        history.Add(done);
                        HandleInput(done);
                    }
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
            string p = Utility.Pipeline("larva");
            foreach (string line in Utility.ReadWrapper(p))
            {
                Console.WriteLine(line);
            }
            Utility.ClearFile(p);
        }
    }
}