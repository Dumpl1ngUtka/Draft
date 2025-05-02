using System.Collections.Generic;
using Grid.Cells;
using Services.GameControlService;
using Services.GameControlService.GridStateMachine;
using UnityEngine;
using Random = System.Random;

namespace Grid.PathMapGrid
{
    public class PathMapGridModel : GridModel
    {
        private readonly int _maxCrossing = 3;
        private List<List<int>> paths;
        
        public List<List<int>> Paths
        {
            get { return paths ??= GeneratePaths(); }
        }

        public PathMapGridModel(GridStateMachine stateMachine) : base(stateMachine)
        {
        }

        private List<List<int>> GeneratePaths()
        {
            var lineCount = GameControlService.Instance.CurrentDungeonInfo.LineCount;
            var columnCount = GameControlService.Instance.CurrentDungeonInfo.ColumnCount;
            var pathCount = GameControlService.Instance.CurrentDungeonInfo.PathCount;
            var random = new Random();
            
            var paths = new List<List<int>>();

            for (int pathIdx = 0; pathIdx < pathCount; pathIdx++)
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
                paths.Add(path);
            }
            return paths;
        }
    }
}