using System.IO;
using System.Text;

namespace LarvaSharp.LarvaLibs
{
    internal class Log
    {
        private string Timestamp { get; }
        private string Name { get; }
        private string Contents { get; }
        private bool UseTimestamp { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param Name="Name">The Name.</param>
        /// <param Name="Contents">The Contents.</param>
        /// <param Name="UseTimestamp">if set to <c>true</c> [use Timestamp].</param>
        public Log(string name, string contents, bool useTimestamp = true)
        {
            if (useTimestamp)
            {
                Timestamp = Utility.Timestamp();
            }
            else
            {
                Timestamp = "";
            }
            UseTimestamp = useTimestamp;
            Name = name;
            Contents = contents;
        }

        /// <summary>
        /// Build the log in string form.
        /// </summary>
        /// <returns>String expression of log.</returns>
        private string Build()
        {
            StringBuilder builder = new StringBuilder();
            if (UseTimestamp)
            {
                builder.Append(Timestamp + ' ');
            }
            builder.AppendFormat("{0}: {1}\n", Name, Contents);
            return builder.ToString();
        }

        /// <summary>
        /// Sends log to larva's output.
        /// </summary>
        public void SendToLarva()
        {
            File.AppendAllText(Utility.Pipeline("larva"), Build());
        }
    }
}