using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using LarvaSharp.LarvaLibs.Modulation;

namespace LarvaSharp.LarvaLibs.Managers
{
    class ProcessManager
    {
        public Process Process { get; private set; }
        Module Module { get; }

        public ProcessManager(Module m)
        {
            Module = m;
            Process = new Process();
            Process.StartInfo.UseShellExecute = false;
            Process.StartInfo.CreateNoWindow = true;
        }

        public bool Alive()
        {
            try
            {
                string c = string.Format("/NH /FO CSV /FI \"PID eq {0}\"", Process.Id); // This is an awful approach.
                return !Utility.RunProcess("TASKLIST.exe", c, true, true).StartsWith("I");
            } catch (InvalidOperationException)
            {
                return false;
            }
        }

        public void Start(string[] args)
        {
            if (Module.Healthy())
            {
                if (Alive())
                {
                    Console.WriteLine("{0}: already running.", Module.Name);
                }
                else
                {
                    if (Module.Extension.Equals("py"))
                    {
                        Process.StartInfo.FileName = "python.exe";
                        Process.StartInfo.Arguments = string.Format("{0} {1} {2}", Module.ExecutablePath, Process.GetCurrentProcess().Id, string.Join(" ", args));
                    } else
                    {
                        Process.StartInfo.FileName = Module.ExecutablePath;
                        Process.StartInfo.Arguments = string.Join("{0} {1}", Process.GetCurrentProcess().Id, string.Join(" ", args));
                    }
                    Process.Start();
                }
            } else
            {
                Console.WriteLine("{0}: module corrupted, missing executablepath.", Module.Name);
            }
        }

        public void Kill()
        {
            if (Alive())
            {
                Process.Kill();
            } else
            {
                Console.WriteLine("{0}: is already dead.");
            }
        }
    }
}
