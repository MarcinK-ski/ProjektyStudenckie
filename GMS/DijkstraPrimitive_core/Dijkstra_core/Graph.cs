using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Dijkstra_core
{
    class Graph
    {
        public int GraphSize { get; private set; }
        Dictionary<int, string> _cities;

        public SortedList<int, List<Edge>> Edges { get; private set; } = new SortedList<int, List<Edge>>();
        public int EdgesNo { get; private set; }

        public Graph(int graphSize)
        {
            GraphSize = graphSize;
            _cities = new Dictionary<int, string>();
        }

        public void AddNewCity(string cityName)
        {
            _cities.Add(_cities.Count, cityName);
        }

        public void AddNewNeighbour(int source, int destination, int weight)
        {
            destination--;
            
            if (weight > 0)
            {
                if(!Edges.ContainsKey(source))
                {
                    Edges.Add(source, new List<Edge>());
                }

                Edges[source].Add(new Edge(source, destination, weight));
            }

            EdgesNo = Edges.Count;
        }

        /// <summary>
        /// Get Cities (src and dest) Indexes
        /// </summary>
        /// <returns>int[0] - source, int[1] - destination</returns>
        private int[] _GetCitiesIndexes(string sourceCity, string destinationCity)
        {
            int source = -1;
            int destination = -1;

            foreach (var city in _cities)
            {
                if (city.Value == sourceCity)
                {
                    source = city.Key;
                }
                else if (city.Value == destinationCity)
                {
                    destination = city.Key;
                }

                if (source > -1 && destination > -1)
                {
                    break;
                }
            }

            return new int[] { source, destination };
        }

        public int FindPatchForCities(string sourceCity, string destinationCity)
        {
            int[] citiesIndexes = _GetCitiesIndexes(sourceCity, destinationCity);
            int source = citiesIndexes[0];
            int destination = citiesIndexes[1];

            return DistanceByDijkstra(source, destination);
        }

        private int DistanceByDijkstra(int source, int destination)
        {
            List<int> distances = new List<int>();   //Odległosci
            List<int> previouses = new List<int>();  //Poprzedniki
            List<int> queueList = new List<int>();

            for (int i = 0; i < GraphSize; i++)
            {
                distances.Add(Int32.MaxValue);
                previouses.Add(-1);

                queueList.Add(i);
            }

            distances[source] = 0;

            while (queueList.Count > 0)
            {

                int minVertex = queueList[0];

                foreach (var item in queueList)
                {
                    if(distances[item] < distances[minVertex])
                    {
                        minVertex = item;
                    }
                }

                queueList.Remove(minVertex);

                if (distances[minVertex] == Int32.MaxValue)
                {
                    break;
                }

                foreach (var edge in Edges[minVertex])
                {
                    int neighbour = edge.Destination;
                    int edgeWeight = edge.Weight;

                    if (edgeWeight > 0)
                    {
                        int alt = distances[minVertex] + edgeWeight;

                        if (alt < distances[neighbour])
                        {
                            distances[neighbour] = alt;
                            previouses[neighbour] = minVertex;
                        }

                    }
                }
            }

            return distances[destination];
        }
    }
}
