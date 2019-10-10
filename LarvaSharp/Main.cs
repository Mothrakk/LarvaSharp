using System;
using System.Threading;
using LarvaSharp.LarvaLibs;

namespace LarvaSharp {
    class Program
    {
        static void Main()
        {
            int i = 0;
            while (true)
            {
                while (!Console.KeyAvailable)
                {
                    Console.WriteLine(i++);
                    Thread.Sleep(1000);
                }
                Console.Write('>');
                Console.ReadLine();
            }
		}
	}
}
