using System;
using System.Collections.Generic;
using System.Text;

namespace _6_colorPlenarGraph_core
{
    class EdgeInt
    {
        public int V1 { get; set; }
        public int V2 { get; set; }

        public EdgeInt(int v1, int v2)
        {
            V1 = v1;
            V2 = v2;
        }
    }

    class EdgeVertex
    {
        public Vertex V1 { get; set; }
        public Vertex V2 { get; set; }

        public EdgeVertex(ref Vertex v1, ref Vertex v2)
        {
            V1 = v1;
            V2 = v2;
        }
    }
}
