using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Dijkstra_core
{
    class TestsReader
    {
        public string FileName { get; private set; }
        public List<Graph> GraphsList { get; private set; }
        public uint CwCount { get; private set; }

        public TestsReader(string fileName)
        {
            FileName = fileName;
            GraphsList = new List<Graph>();

            ReadTests();
        }

        private void ReadTests()
        {
            using (StreamReader reader = new StreamReader(FileName))
            {
                int testCasesCount = Int32.Parse(reader.ReadLine());

                for (int i = 0; i < testCasesCount; i++)
                {
                    int citiesNumber = Int32.Parse(reader.ReadLine());
                    GraphsList.Add(new Graph(citiesNumber));

                    for (int c = 0; c < citiesNumber; c++)
                    {
                        GraphsList[i].AddNewCity(reader.ReadLine());
                        int howManyNeighbours = Int32.Parse(reader.ReadLine());
                        for (int neigh = 0; neigh < howManyNeighbours; neigh++)
                        {
                            string[] neighbourDetails = reader.ReadLine().Trim().Split(' ');
                            GraphsList[i].AddNewNeighbour(c, Int32.Parse(neighbourDetails[0]), Int32.Parse(neighbourDetails[1]));
                        }
                    }

                    int pathToFindNumber = Int32.Parse(reader.ReadLine());

                    for (int p = 0; p < pathToFindNumber; p++)
                    {
                        string[] citiesToFindPatch = reader.ReadLine().Trim().Split(' ');

                        Console.WriteLine(GraphsList[i].FindPatchForCities(citiesToFindPatch[0], citiesToFindPatch[1]));
                        CwCount++;
                    }

                    reader.ReadLine();
                }
            }
        }
    }
}
