using System;
using System.Collections.Generic;
using System.Text;

namespace BellmanFord_core
{
    class Edge
    {
        public int Source { get; private set; }
        public int Destination { get; private set; }
        public int Weight { get; private set; }

        public Edge(int src, int dst, int wght)
        {
            Source = src;
            Destination = dst;
            Weight = wght;
        }
    }
}
