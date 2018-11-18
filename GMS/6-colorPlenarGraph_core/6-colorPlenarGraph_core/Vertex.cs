using System;
using System.Collections.Generic;
using System.Text;

namespace _6_colorPlenarGraph_core
{
    class Vertex
    {
        public int VertexName { get; private set; }
        public List<int> NeighboursNames { get; private set; } = new List<int>();
        public int Degree { get; private set; }
        public bool IsColored { get; set; }
        public sbyte Color { get; set; } = -1;

        public Vertex(int name)
        {
            VertexName = name;
        }

        public override string ToString()
        {
            return $"{VertexName} -> {Degree}";
        }

        public void AddNeighbour(int neighbourName)
        {
            NeighboursNames.Add(neighbourName);

            Degree = NeighboursNames.Count;
        }

        public void RemoveNeighbour(int neighbourName)
        {
            NeighboursNames.Remove(neighbourName);

            Degree = NeighboursNames.Count;
        }

        public bool HasNeighbour(int neighbourName)
        {
            if(NeighboursNames.Contains(neighbourName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    internal class VertexComparer : IComparer<Vertex>
    {
        public int Compare(Vertex v1, Vertex v2)
        {
            int result = v1.Degree.CompareTo(v2.Degree);

            if (result == 0)
                result = v1.VertexName.CompareTo(v2.VertexName);

            return result;
        }
    }
}
