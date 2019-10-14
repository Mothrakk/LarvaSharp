using System;
using System.IO;
using LarvaSharp.LarvaLibs.Commanding;
using LarvaSharp.LarvaLibs.Managers;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    class Logo : CommandInterface
    {
        public string HelpText()
        {
            return "Print out a logo from the 'logos' folder, if possible.";
        }

        public void Run(string[] args, ManagerInfo managerInfo = null)
        {
            if (Directory.Exists("logos"))
            {
                string[] files = Directory.GetFiles("logos", "*.txt");
                if (files.Length > 0)
                {
                    Random rand = new Random();
                    int r = rand.Next(0, files.Length);
                    Console.WriteLine(File.ReadAllText(files[r]));
                }
            }
        }
    }
}
