using System;
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
        /// Returns a <see cref="string"/> timestamp of the current local time in the form of "HH:mm:ss".
        /// </summary>
        /// <returns><see cref="string"/> in the form of "HH:mm:ss".</returns>
        public static string Timestamp()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

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
        /// Read and return the contents from the given path. Overwrite the file afterwards and leave it empty.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static string[] Flush(string path)
        {
            List<string> linesList = new List<string>();
            if (File.Exists(path))
            {
                linesList.AddRange(File.ReadAllLines(path));
                File.WriteAllText(path, "");
            }
            return linesList.ToArray();
        }

        /// <summary>
        /// One function for Flush(Pipeline(x, y)).
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="extension">The extension.</param>
        /// <returns>File lines.</returns>
        public static string[] FlushPipeline(string name, string extension = ".txt")
        {
            return Flush(Pipeline(name, extension));
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
        public static string RelativePath(string p)
        {
            return string.Format("{0}\\{1}", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), p);
        }
    }
}