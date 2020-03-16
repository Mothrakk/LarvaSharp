using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace LarvaSharp.LarvaLibs
{
    internal static class Utility
    {
        /// <summary>
        /// Sleep for <paramref name="milliseconds"/> and then return true.
        /// </summary>
        /// <param name="milliseconds">The milliseconds.</param>
        /// <returns>True.</returns>
        public static bool Tick(int milliseconds)
        {
            Thread.Sleep(milliseconds);
            return true;
        }

        /// <summary>
        /// Build and return a path to the given filename inside the 'pipeline' folder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="extension">The extension.</param>
        /// <returns>The path.</returns>
        public static string Pipeline(string name, string extension = ".txt")
        {
            return RelativePath(string.Format("pipeline\\{0}{1}", name, extension));
        }

        /// <summary>
        /// Clears the file.
        /// </summary>
        /// <param name="path">The path.</param>
        public static void ClearFile(string path)
        {
            if (File.Exists(path))
            {
                File.WriteAllText(path, "");
            }
        }

        /// <summary>
        /// Runs the process.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="getOutput">if set to <c>true</c> [get output].</param>
        /// <param name="createNoWindow">if set to <c>true</c> [create no window].</param>
        /// <returns></returns>
        public static string RunProcess(string filename, string args, bool getOutput = false, bool createNoWindow = false)
        {
            Process p = new Process();
            string o = null;
            p.StartInfo.FileName = filename;
            p.StartInfo.Arguments = args;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = getOutput;
            p.StartInfo.CreateNoWindow = createNoWindow;
            p.Start();
            p.WaitForExit();
            if (getOutput)
            {
                o = p.StandardOutput.ReadToEnd().Trim();
            }
            p.Close();
            return o;
        }

        /// <summary>
        /// Build the relative path where the assembly file of Larva is the parent.
        /// </summary>
        /// <param name="p">The path to append.</param>
        /// <returns></returns>
        public static string RelativePath(string p = "")
        {
            return string.Format("{0}\\{1}", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), p);
        }

        /// <summary>
        /// Safety wrapper for reading a file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static string[] ReadWrapper(string path)
        {
            while (true)
            {
                try
                {
                    return File.ReadAllLines(path);
                } catch (IOException) {}
            }
        }
    }
}