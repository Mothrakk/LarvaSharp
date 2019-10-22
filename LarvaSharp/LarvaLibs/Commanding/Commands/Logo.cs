using LarvaSharp.LarvaLibs.Modulation;
using System;
using System.IO;

namespace LarvaSharp.LarvaLibs.Commanding.Commands
{
    internal class Logo : ICommandInterface
    {
        public string HelpText()
        {
            return "Print out a logo from the 'logos' folder, if possible.";
        }

        public void Run(string[] args, ManagerCollection managerCollection)
        {
            if (Directory.Exists(Utility.RelativePath("logos")))
            {
                string[] files = Directory.GetFiles(Utility.RelativePath("logos"), "*.txt");
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