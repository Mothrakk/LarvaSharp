using System.IO;
using System.Linq;

namespace LarvaSharp.LarvaLibs.Configuration
{
    /// <summary>
    /// Class where each instance is meant for one configuration file (autostart.cfg, etc).
    /// </summary>
    internal class ConfigItem
    {
        private string Path { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigItem"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        internal ConfigItem(string filename)
        {
            Path = Utility.RelativePath(string.Format("config\\{0}.cfg", filename));
            if (!File.Exists(Path))
            {
                File.WriteAllText(Path, "");
            }
        }

        /// <summary>
        /// Appends the specified contents.
        /// </summary>
        /// <param name="contents">The contents.</param>
        public void Append(string contents)
        {
            if (!contents.EndsWith("\n"))
            {
                contents += '\n';
            }
            File.AppendAllText(Path, contents);
        }

        /// <summary>
        /// Reads the file's contents.
        /// </summary>
        /// <returns></returns>
        public string[] Read() => Utility.ReadWrapper(Path);

        /// <summary>
        /// Determines whether the file contains the given item.
        /// If <paramref name="fullMatch"/> is false, any of the file's lines only need to start with <paramref name="item"/>. Else, it must be a full match.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="fullMatch">if set to <c>true</c> [full match].</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(string item, bool fullMatch = false)
        {
            return Read()
                   .Any(line => fullMatch ? line == item : line.StartsWith(item));
        }

        /// <summary>
        /// Erases the specified item from the file.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="fullMatch">if set to <c>true</c> [full match].</param>
        public void Erase(string item, bool fullMatch = false)
        {
            File.WriteAllLines(Path,
                Read()
                .Where(line => fullMatch ? line != item : !line.StartsWith(item))
            );
        }

        /// <summary>
        /// Empties the file.
        /// </summary>
        public void Clear() => Utility.ClearFile(Path);
    }
}