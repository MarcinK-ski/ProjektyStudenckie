using System;

namespace Euler_core
{
    class Program
    {
        static void Main(string[] args)
        {
            InputReader input = new InputReader("in2");

            Graph graph;
            while ((graph = input.FetchGraph()) != null)
            {
                foreach (var item in graph.GetEulerStack())
                {
                    Console.Write(item + " ");
                }

                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
