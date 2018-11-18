using System;
using System.Diagnostics;

namespace StabilneMalzenstwa_core
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();
            TestReader tests = new TestReader("mtp_in_public");

            //Console.ReadKey();
            sw.Stop();
            Console.WriteLine("TIME: " + sw.ElapsedMilliseconds);
        }
    }
}
