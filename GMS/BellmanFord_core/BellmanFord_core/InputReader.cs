using System;
using System.IO;

namespace BellmanFord_core
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
                    int verticesNumber = Int32.Parse(reader.ReadLine());
                    _graphs[i] = new Graph(verticesNumber);

                    for(int src = 0; src < verticesNumber; src++)
                    {
                        int[] edges = Array.ConvertAll(reader.ReadLine().Trim().Split(' '), int.Parse);
                        _graphs[i].AddNewEdges(src, edges);
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
