using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LarvaSharp.LarvaLibs
{
    static class Utility
    {
        public static string Timestamp()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
