using System;
using System.Collections.Generic;
using System.Linq;

namespace BellmanFord_core
{
    class Graph
    {
        public const int INFINITY = Int32.MaxValue - 10000;
        public int VerticesNo { get; private set; }
        public int EdgesNo { get; private set; }
        public List<Edge> Edges { get; private set; } = new List<Edge>();

        public Graph(int verticesNumber)
        {
            VerticesNo = verticesNumber;
        }

        public void AddNewEdges(int src, int[] edges)
        {
            for(int destination = 0; destination < edges.Length; destination++)
            {
                int edgeWeight = edges[destination];

                if(edgeWeight != 0)
                {
                    Edges.Add(new Edge(src, destination, edgeWeight));
                }
            }

            EdgesNo = Edges.Count;
        }

        public void BellmanFordAlgo()
        {
            int[] pathTo = Enumerable.Repeat(INFINITY, VerticesNo).ToArray();
            pathTo[0] = 0;

            for (int i = 1; i < VerticesNo; i++)
            {
                for (int j = 0; j < EdgesNo; j++)
                {
                    var edge = Edges[j];

                    if(pathTo[edge.Source] < INFINITY)
                    {
                        if (pathTo[edge.Source] + edge.Weight < pathTo[edge.Destination])
                        {
                            pathTo[edge.Destination] = pathTo[edge.Source] + edge.Weight;
                        }
                    }
                }

                string result = "";
                foreach(var distance in pathTo)
                {
                    if (distance == INFINITY)
                    {
                        result += "0 ";
                    }
                    else
                    {
                        result += $"{distance} ";
                    }
                }
                Console.WriteLine(result);
            }

            Console.WriteLine();
        }
    }
}
