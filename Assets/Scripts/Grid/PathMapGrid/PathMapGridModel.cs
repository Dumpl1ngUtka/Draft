using System;
using System.Collections.Generic;
using DungeonMap;
using Grid.Cells;
using Services.GameControlService;
using UnityEngine;
using Random = System.Random;

namespace Grid.PathMapGrid
{
    public class PathMapGridModel : GridModel
    {
        private int[,] _map;

        private DungeonInfo _currentDungeonInfo => GameControlService.Instance.CurrentDungeonInfo;
        private int _lineCount => _currentDungeonInfo.LineCount;
        private int _columnCount => _currentDungeonInfo.ColumnCount;

        public int[,] Map
        {
            get { return _map ??= GenerateMap(); }
        }

        public int[] Path => GameControlService.CurrentRunInfo.Path;

        private readonly Random _random;

        public PathMapGridModel()
        {
            if (GameControlService.CurrentRunInfo.PathSeed == 0)
                GameControlService.CurrentRunInfo.PathSeed = (int) DateTime.Now.Ticks & 0x0000FFFF;
            
            _random = new Random(GameControlService.CurrentRunInfo.PathSeed);
        }

        private int[,] GenerateMap()
        {
            var map = new int[_lineCount, _columnCount];
            var activePaths = new List<int>();
            
            for (int i = 0; i < _currentDungeonInfo.StartPointCount; i++)
            {
                int startPos;
                do
                {
                    startPos = _random.Next(0, _columnCount);
                } while (map[0, startPos] == 1);

                map[0, startPos] = 1;
                activePaths.Add(startPos);
            }

            for (int row = 1; row < _lineCount; row++)
            {
                var newActivePaths = new List<int>();

                foreach (var pathPos in activePaths)
                {
                    int minNextPos = Math.Max(0, pathPos - 1);
                    int maxNextPos = Math.Min(_columnCount - 1, pathPos + 1);

                    int nextPosition = _random.Next(minNextPos, maxNextPos + 1);

                    map[row, nextPosition] = 1;
                    if (!newActivePaths.Contains(nextPosition))
                        newActivePaths.Add(nextPosition);

                    if (_random.NextDouble() < _currentDungeonInfo.BranchingChance && minNextPos != maxNextPos)
                    {
                        int branchPos;
                        do
                        {
                            branchPos = _random.Next(minNextPos, maxNextPos + 1);
                        } while (branchPos == nextPosition);

                        map[row, branchPos] = 1;
                        if (!newActivePaths.Contains(branchPos))
                            newActivePaths.Add(branchPos);
                    }
                }

                activePaths = newActivePaths;

                if (activePaths.Count == 0)
                {
                    int newPathPos = _random.Next(0, _columnCount);
                    map[row, newPathPos] = 1;
                    activePaths.Add(newPathPos);
                }
            }

            for (int lineIndex = 0; lineIndex < _lineCount - 1; lineIndex++)
            {
                for (int columnIndex = 0; columnIndex < _columnCount; columnIndex++)
                {
                    if (map[lineIndex, columnIndex] == 0 && _random.NextDouble() < 0.15)
                    {
                        bool hasConnectionAbove = false;
                        bool hasConnectionBelow = false;

                        if (lineIndex > 0)
                        {
                            int minAbove = Math.Max(0, columnIndex - 1);
                            int maxAbove = Math.Min(_columnCount - 1, columnIndex + 1);
                            for (int aboveCol = minAbove; aboveCol <= maxAbove; aboveCol++)
                            {
                                if (map[lineIndex - 1, aboveCol] == 1)
                                {
                                    hasConnectionAbove = true;
                                    break;
                                }
                            }
                        }

                        if (lineIndex < _lineCount - 1)
                        {
                            int minBelow = Math.Max(0, columnIndex - 1);
                            int maxBelow = Math.Min(_columnCount - 1, columnIndex + 1);
                            for (int belowCol = minBelow; belowCol <= maxBelow; belowCol++)
                            {
                                if (map[lineIndex + 1, belowCol] == 1)
                                {
                                    hasConnectionBelow = true;
                                    break;
                                }
                            }
                        }

                        if (hasConnectionAbove && hasConnectionBelow)
                        {
                            map[lineIndex, columnIndex] = 1;
                        }
                    }
                }
            }

            for (int lineIndex = 0; lineIndex < map.GetLength(0); lineIndex++)
            {
                for (int columnIndex = 0; columnIndex < map.GetLength(1); columnIndex++)
                {
                    if (map[lineIndex, columnIndex] == 1)
                    {
                        map[lineIndex, columnIndex] = (int)_currentDungeonInfo.GetRandomPathCellType(lineIndex,_random);
                    }
                }
            }

            return map;
        }

        public void SelectPathCell(PathMapCell pathCell)
        {
            if (pathCell.PathCellType == PathCellType.Monsters)
            {
                GameControlService.ChangeGrid(GameControlService.BattleGridPrefab);
                GameControlService.CurrentRunInfo.UpdatePath(pathCell.Position.ColumnIndex);
            }
        }
    }
}