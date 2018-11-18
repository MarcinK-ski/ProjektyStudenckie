using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;

namespace Dijkstra_core
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();

            TestsReader tests = new TestsReader("input");

            sw.Stop();
            Console.WriteLine("TIME: " + sw.ElapsedMilliseconds);

            Console.ReadKey();            
        }
    }
}
