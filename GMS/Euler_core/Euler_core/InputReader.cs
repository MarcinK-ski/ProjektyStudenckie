using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Euler_core
{
    class InputReader
    {
        private string _testsPathFile;
        private int _testsSum;
        private int _testNo;
        private Graph[] _graphs;

        public InputReader(string filePath)
        {
            _testsPathFile = filePath;
            ReadTestsFile();
        }

        private void ReadTestsFile()
        {
            using (StreamReader reader = new StreamReader(_testsPathFile))
            {
                _testsSum = Int32.Parse(reader.ReadLine());
                _graphs = new Graph[_testsSum];

                for(int i = 0; i < _testsSum; i++)
                {
                    string firstParam = reader.ReadLine();
                    string secondParam = reader.ReadLine();

                    Regex verticesAndEdgesQuantity = new Regex(@"n=(?<n>\d+),m=(?<m>\d+)");
                    var verAndEdgMatch = verticesAndEdgesQuantity.Match(firstParam);

                    if(verAndEdgMatch.Success)
                    {
                        if(verAndEdgMatch?.Groups["n"] != null && verAndEdgMatch?.Groups["m"] != null)
                        {
                            int verticesQuantiy = Int32.Parse(verAndEdgMatch.Groups["n"].Value);
                            int edgesQuantity = Int32.Parse(verAndEdgMatch.Groups["m"].Value);

                            _graphs[i] = new Graph(verticesQuantiy, edgesQuantity);
                        }
                        else
                        {
                            Console.WriteLine($"Regex nie dopasował n lub m dla {firstParam} w iteracji {i}");
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Pattern regexa nie dopasował nic dla {firstParam} w iteracji {i}");
                        continue;
                    }


                    Regex unprocessedEdgesList = new Regex(@"(?<u>\d+),(?<v>\d+)");
                    var edgesMatches = unprocessedEdgesList.Matches(secondParam);

                    foreach(Match edgeMatch in edgesMatches)
                    {
                        if (edgeMatch.Success)
                        {
                            if (edgeMatch?.Groups["u"] != null && edgeMatch?.Groups["v"] != null)
                            {
                                int vertex = Int32.Parse(edgeMatch.Groups["u"].Value);
                                int neighbour = Int32.Parse(edgeMatch.Groups["v"].Value);

                                _graphs[i].AddNewNeighbour(vertex, neighbour);
                            }
                            else
                            {
                                Console.WriteLine($"Regex nie dopasował u lub v przy value: {edgeMatch.Value} dla {secondParam} w iteracji {i}");
                                continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Pattern regexa nie dopasował nic dla {secondParam} w iteracji {i}");
                            continue;
                        }
                    }
                }
            }
        }

        public Graph FetchGraph()
        {
            if (_testsSum > _testNo)
            {
                return _graphs[_testNo++];
            }
            else
            {
                return null;
            }
        }
    }
}
