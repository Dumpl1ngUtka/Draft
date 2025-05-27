using System.Collections.Generic;
using DungeonMap;
using Grid.Cells;
using Services.GameControlService;
using Services.GameControlService.GridStateMachine;
using UnityEngine;
using Random = System.Random;

namespace Grid.PathMapGrid
{
    public class PathMapGridModel : GridModel
    {
        private const int _maxCrossing = 3;
        
        private List<List<int>> _paths;
        private List<List<PathCellType>> _cellsTypes;
        
        private DungeonInfo _currentDungeonInfo => GameControlService.Instance.CurrentDungeonInfo;
        private int _lineCount => _currentDungeonInfo.LineCount;
        private int _columnCount => _currentDungeonInfo.ColumnCount;
        private int _pathCount => _currentDungeonInfo.PathCount;
        
        public List<List<int>> Paths
        {
            get { return _paths ??= GeneratePaths(); }
        }
        public List<List<PathCellType>> CellsTypes
        {
            get { return _cellsTypes ??= GenerateCellTypes(); }
        }

        public PathMapGridModel()
        {
            //GameControlService.CurrentRunInfo.Path
        }

        private List<List<PathCellType>> GenerateCellTypes()
        {
            var cellTypes = new List<List<PathCellType>>();
            for (int line = 0; line < _lineCount; line++)
            {
                var cellLine = new List<PathCellType>();
                for (int column = 0; column < _columnCount; column++)
                {
                    var type = _currentDungeonInfo.GetRandomPathCellType(line);
                    cellLine.Add(type);
                }
                cellTypes.Add(cellLine);
            }

            return cellTypes;
        }

        private List<List<int>> GeneratePaths()
        {
            var random = new Random();
            
            var paths = new List<List<int>>();

            for (int pathIdx = 0; pathIdx < _pathCount; pathIdx++)
            {
                var path = new List<int>();
                path.Add(random.Next(_columnCount));

                for (int i = 1; i < _lineCount; i++)
                {
                    var validColumns = new List<int>();
                    for (int col = 0; col < _columnCount; col++)
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

        public void SelectPathCell(PathMapCell pathCell)
        {
            if (pathCell.PathCellType == PathCellType.Monsters)
            {
                GameControlService.ChangeGrid(GameControlService.BattleGridPrefab);
            }
        }
    }
}