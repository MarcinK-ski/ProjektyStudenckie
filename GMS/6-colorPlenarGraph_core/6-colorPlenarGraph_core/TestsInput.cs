using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace _6_colorPlenarGraph_core
{
    class TestsInput
    {
        public TestsInput(string file)
        {
            _TestsReader(file);
        }

        private void _TestsReader(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                byte testsCount = byte.Parse(reader.ReadLine());

                for (byte i = 0; i < testsCount; i++)
                {
                    string nodesAndEdgesNumberAsPlainText = reader.ReadLine();
                    Regex nodesAndEdgesRegex = new Regex(@"Graph with (?<nodes>\d+) nodes and (?<edges>\d+) edges");
                    var nodesAndEdgesMatch = nodesAndEdgesRegex.Match(nodesAndEdgesNumberAsPlainText);

                    if(nodesAndEdgesMatch.Success)
                    {
                        int nodesCount = int.Parse(nodesAndEdgesMatch.Groups["nodes"].Value);
                        int edgesCount = int.Parse(nodesAndEdgesMatch.Groups["edges"].Value);

                        string unprocessedEdgesAsPlainText = reader.ReadLine();
                        Regex unprocessedEdgesListRegex = new Regex(@"(?<u>\d+),(?<v>\d+)");
                        var edgesMatches = unprocessedEdgesListRegex.Matches(unprocessedEdgesAsPlainText);

                        Graph graph = new Graph();

                        foreach (Match edgeMatch in edgesMatches)
                        {
                            if (edgeMatch.Success)
                            {
                                if (edgeMatch?.Groups["u"] != null && edgeMatch?.Groups["v"] != null)
                                {
                                    int vertexName = Int32.Parse(edgeMatch.Groups["u"].Value);
                                    int neighbourName = Int32.Parse(edgeMatch.Groups["v"].Value);

                                    graph.Edges.Add(new EdgeInt(vertexName, neighbourName));
                                }
                                else
                                {
                                    Console.WriteLine($"Regex nie dopasował u lub v przy value: {edgeMatch.Value} dla {unprocessedEdgesAsPlainText} w iteracji {i}");
                                    continue;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Pattern regexa nie dopasował nic dla {unprocessedEdgesAsPlainText} w iteracji {i}");
                                continue;
                            }
                        }

                        graph.SmallestLastAlgorithm();
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine($"Problem z regexem dla: {nodesAndEdgesNumberAsPlainText}");
                        reader.ReadLine();  //to skip edges list 
                    }
                }
            }
        }
    }
}
