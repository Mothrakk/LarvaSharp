using System;
using System.Diagnostics;

namespace LarvaSharp.LarvaLibs.Modulation
{
    internal class ProcessHandler
    {
        private Process Process { get; set; }
        private Module Module { get; }

        public ProcessHandler(Module m)
        {
            Module = m;
            Process = new Process();
            Process.StartInfo.UseShellExecute = false;
            Process.StartInfo.CreateNoWindow = false;
        }

        public bool Alive()
        {
            try
            {
                string c = string.Format("/NH /FO CSV /FI \"PID eq {0}\"", Process.Id); // This is an awful approach.
                return !Utility.RunProcess("TASKLIST.exe", c, true, true).StartsWith("I");
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public void Start(string[] args)
        {
            string joinedArgs = string.Join(" ", args);
            if (Module.Healthy())
            {
                if (Alive())
                {
                    Console.WriteLine("{0}: already running.", Module.Name);
                }
                else
                {
                    switch (Module.Extension)
                    {
                        case ".py":
                            Process.StartInfo.FileName = "python.exe";
                            Process.StartInfo.Arguments = string.Format("{0} {1}", Module.ExecutablePath, joinedArgs);
                            break;

                        case ".exe":
                            Process.StartInfo.FileName = Module.ExecutablePath;
                            Process.StartInfo.Arguments = joinedArgs;
                            break;
                    }

                    Process.Start();
                    Console.WriteLine("Started " + Module.Name);
                }
            }
            else
            {
                Console.WriteLine("{0}: module corrupted, missing executablepath.", Module.Name);
            }
        }

        public void Kill()
        {
            if (Alive())
            {
                Process.Kill();
                Console.WriteLine("Killed {0}.", Module.Name);
            }
            else
            {
                Console.WriteLine("{0} is already dead.", Module.Name);
            }
        }
    }
}