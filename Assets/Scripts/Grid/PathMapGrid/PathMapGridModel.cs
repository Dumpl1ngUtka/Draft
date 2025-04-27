using System.Collections.Generic;
using Grid.Cells;
using UnityEngine;
using Random = System.Random;

namespace Grid.PathMapGrid
{
    public class PathMapGridModel : GridModel
    {
        private readonly int _maxCrossing = 3;
        private readonly int _pathCount = 3;
        
        public void GeneratePathsFor(List<List<PathMapCell>> cells)
        {
            var lineCount = cells.Count;
            var columnCount = cells[0].Count;
            var random = new Random();

            for (int pathIdx = 0; pathIdx < _pathCount; pathIdx++)
            {
                var path = new List<int>();
                path.Add(random.Next(columnCount - 1));

                for (int i = 1; i < lineCount; i++)
                {
                    var validColumns = new List<int>();
                    for (int col = 0; col < columnCount; col++)
                        validColumns.Add(col);

                    for (int crossingLen = 1; crossingLen <= _maxCrossing; crossingLen++)
                    {
                        if (i >= crossingLen)
                        {
                            bool allSame = true;
                            for (int j = 1; j < crossingLen; j++)
                            {
                                if (path[i - j] != path[i - j - 1])
                                {
                                    allSame = false;
                                    break;
                                }
                            }

                            if (allSame && crossingLen == _maxCrossing)
                            {
                                validColumns.Remove(path[i - 1]);
                            }
                        }
                    }

                    path.Add(validColumns[random.Next(validColumns.Count)]);
                }

                for (int lineIndex = 1; lineIndex < lineCount; lineIndex++)
                {
                    cells[lineIndex - 1][path[lineIndex - 1]].SetNextCell(cells[lineIndex][path[lineIndex]]);
                }
            }
        }
    }
}