using System.Text;
using System.IO;

namespace LarvaSharp.LarvaLibs
{
    class Log
    {
        readonly string timestamp;
        readonly string name;
        readonly string contents;
        readonly bool useTimestamp;

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="useTimestamp">if set to <c>true</c> [use timestamp].</param>
        public Log(string name, string contents, bool useTimestamp=true)
        {
            if (useTimestamp)
            {
                timestamp = Utility.Timestamp();
            } else
            {
                timestamp = "";
            }
            this.useTimestamp = useTimestamp;
            this.name = name;
            this.contents = contents;
        }

        /// <summary>
        /// Build the log in string form.
        /// </summary>
        /// <returns>String expression of log.</returns>
        string Build()
        {
            StringBuilder builder = new StringBuilder();
            if (useTimestamp)
            {
                builder.Append(timestamp + ' ');
            }
            builder.AppendFormat("{0}: {1}\n", name, contents);
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
