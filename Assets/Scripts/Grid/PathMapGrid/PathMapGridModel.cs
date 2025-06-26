using System;
using System.Collections.Generic;
using DungeonMap;
using Grid.Cells;
using Services.SaveLoadSystem;
using Random = System.Random;

namespace Grid.PathMapGrid
{
    public class PathMapGridModel : GridModel
    {
        private readonly RunData _runData;
        
        public int[,] Map { get; }

        public int[] Path { get; }


        public PathMapGridModel()
        {
            _runData = SaveLoadService.Instance.LoadRunData();
            
            var random = new Random(SaveLoadService.Instance.LoadRunData().PathSeed);
            var dungeonInfo = DungeonInfo.GetObjectByID(_runData.DungeonID);
            
            Map = GenerateMap(random, dungeonInfo);
            Path = _runData.GetPath();
        }

        private static int[,] GenerateMap(Random random, DungeonInfo dungeonInfo)
        {
            var lineCount = dungeonInfo.LineCount;
            var columnCount = dungeonInfo.ColumnCount;
            var map = new int[lineCount, columnCount];
            var activePaths = new List<int>();
            
            for (int i = 0; i < dungeonInfo.StartPointCount; i++)
            {
                int startPos;
                do
                {
                    startPos = random.Next(0, columnCount);
                } while (map[0, startPos] == 1);

                map[0, startPos] = 1;
                activePaths.Add(startPos);
            }

            for (int row = 1; row < lineCount; row++)
            {
                var newActivePaths = new List<int>();

                foreach (var pathPos in activePaths)
                {
                    int minNextPos = Math.Max(0, pathPos - 1);
                    int maxNextPos = Math.Min(columnCount - 1, pathPos + 1);

                    int nextPosition = random.Next(minNextPos, maxNextPos + 1);

                    map[row, nextPosition] = 1;
                    if (!newActivePaths.Contains(nextPosition))
                        newActivePaths.Add(nextPosition);

                    if (random.NextDouble() < dungeonInfo.BranchingChance && minNextPos != maxNextPos)
                    {
                        int branchPos;
                        do
                        {
                            branchPos = random.Next(minNextPos, maxNextPos + 1);
                        } while (branchPos == nextPosition);

                        map[row, branchPos] = 1;
                        if (!newActivePaths.Contains(branchPos))
                            newActivePaths.Add(branchPos);
                    }
                }

                activePaths = newActivePaths;

                if (activePaths.Count == 0)
                {
                    int newPathPos = random.Next(0, columnCount);
                    map[row, newPathPos] = 1;
                    activePaths.Add(newPathPos);
                }
            }

            for (int lineIndex = 0; lineIndex < lineCount - 1; lineIndex++)
            {
                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    if (map[lineIndex, columnIndex] == 0 && random.NextDouble() < 0.15)
                    {
                        bool hasConnectionAbove = false;
                        bool hasConnectionBelow = false;

                        if (lineIndex > 0)
                        {
                            int minAbove = Math.Max(0, columnIndex - 1);
                            int maxAbove = Math.Min(columnCount - 1, columnIndex + 1);
                            for (int aboveCol = minAbove; aboveCol <= maxAbove; aboveCol++)
                            {
                                if (map[lineIndex - 1, aboveCol] == 1)
                                {
                                    hasConnectionAbove = true;
                                    break;
                                }
                            }
                        }

                        if (lineIndex < lineCount - 1)
                        {
                            int minBelow = Math.Max(0, columnIndex - 1);
                            int maxBelow = Math.Min(columnCount - 1, columnIndex + 1);
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
                        map[lineIndex, columnIndex] = (int)dungeonInfo.GetRandomPathCellType(lineIndex,random);
                    }
                }
            }

            return map;
        }

        public void SelectPathCell(PathMapCell pathCell)
        {
            if (pathCell.PathCellType == PathCellType.Monsters)
            {
                _runData.UpdatePath(pathCell.Position.ColumnIndex);
                SaveLoadService.Instance.SaveRunData(_runData);
                GameControlService.ChangeGrid(GameControlService.BattleGridPrefab);
            }
        }
    }
}