using System;
using System.Collections.Generic;
using System.Linq;

namespace Euler_core
{
    class Graph
    {
        public const int INFINITY = Int32.MaxValue - 10000;
        public int VerticesNo { get; private set; }
        public int EdgesNo { get; private set; }
        public sbyte[,] neighboursMatrix;
        private Stack<int> _eulerStack;

        public Graph(int verticesNumber, int edgesNumber)
        {
            VerticesNo = verticesNumber;
            EdgesNo = edgesNumber;

            neighboursMatrix = new sbyte[verticesNumber, verticesNumber];
            _eulerStack = new Stack<int>(edgesNumber + 1);
        }

        public void AddNewNeighbour(int vertex, int neighbour)
        {
            neighboursMatrix[vertex, neighbour] = 1;
            neighboursMatrix[neighbour, vertex] = 1;
        }

        public void RemoveNeighbour(int vertex, int neighbour)
        {
            neighboursMatrix[vertex, neighbour] = 0;
            neighboursMatrix[neighbour, vertex] = 0;
        }

        private void GetEulerCycle(int vertex)
        {
            for(int i = 0; i < VerticesNo; i++)
            {
                if(neighboursMatrix[vertex, i] > 0)
                {
                    RemoveNeighbour(vertex, i);
                    GetEulerCycle(i);
                }
            }

            _eulerStack.Push(vertex);
        }

        public Stack<int> GetEulerStack()
        {
            GetEulerCycle(0);

            return _eulerStack;
        }
    }
}
