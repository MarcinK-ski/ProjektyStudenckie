using System;
using System.Collections.Generic;
using System.Text;

namespace _6_colorPlenarGraph_core
{
    class Graph
    {
        public List<EdgeInt> Edges { get; private set; } = new List<EdgeInt>();
        public SortedList<int, Vertex> VerticesList { get; private set; } = new SortedList<int, Vertex>();

        public Graph()
        {   }

        private void _GenerateVerticesList()
        {
            foreach (var edge in Edges)
            {
                if(!VerticesList.ContainsKey(edge.V1))
                {
                    VerticesList.Add(edge.V1, new Vertex(edge.V1));
                }

                VerticesList[edge.V1].AddNeighbour(edge.V2);


                if (!VerticesList.ContainsKey(edge.V2))
                {
                    VerticesList.Add(edge.V2, new Vertex(edge.V2));
                }

                if(!VerticesList[edge.V2].HasNeighbour(edge.V1))
                {
                    VerticesList[edge.V2].AddNeighbour(edge.V1);
                }
            }
        }

        private void _Coloring(int vertexName)
        {
            Vertex currentVertex = VerticesList[vertexName];

            if (!currentVertex.IsColored)
            {
                SortedSet<sbyte> availableColors = new SortedSet<sbyte>() { 1, 2, 3, 4, 5, 6 };

                foreach(var neighbourVertex in VerticesList.Values)
                {
                    if (neighbourVertex.VertexName != currentVertex.VertexName && neighbourVertex.IsColored
                        && currentVertex.NeighboursNames.Contains(neighbourVertex.VertexName))
                    {
                        availableColors.Remove(neighbourVertex.Color);
                    }
                }

                currentVertex.IsColored = true;
                currentVertex.Color = availableColors.Min;
            }

            Console.WriteLine($"{currentVertex.VertexName} {currentVertex.Color}");
        }

        public void SmallestLastAlgorithm()
        {
            _GenerateVerticesList();

            List<Vertex> verticesWithDeleting = new List<Vertex>(VerticesList.Values);

            while(verticesWithDeleting.Count > 0)
            {
                SortedSet<Vertex> degreesAndVertex = new SortedSet<Vertex>(new VertexComparer());

                for (int i = 0; i < verticesWithDeleting.Count; i++)
                {
                    Vertex currentVertex = verticesWithDeleting[i];

                    degreesAndVertex.Add(currentVertex);
                }

                var minimumVertex = degreesAndVertex.Min;

                verticesWithDeleting.Remove(minimumVertex);

                for (int v = 0; v < verticesWithDeleting.Count; v++)
                {
                    if(verticesWithDeleting[v].HasNeighbour(minimumVertex.VertexName))
                    {
                        verticesWithDeleting[v].RemoveNeighbour(minimumVertex.VertexName);
                    }
                }

                _Coloring(minimumVertex.VertexName);
            }
        }
    }
}
