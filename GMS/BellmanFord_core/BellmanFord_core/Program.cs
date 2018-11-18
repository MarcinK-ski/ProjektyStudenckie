using System;

namespace BellmanFord_core
{
    class Program
    {
        static void Main(string[] args)
        {
            InputReader read = new InputReader("in");
            
            Graph graph;

            while ((graph = read.FetchGraph()) != null)
            {
                graph.BellmanFordAlgo();
            }
        }
    }
}
