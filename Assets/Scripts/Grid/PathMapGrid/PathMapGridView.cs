using System;
using System.Collections.Generic;
using DungeonMap;
using Grid.Cells;
using UnityEngine;
using UnityEngine.UI;

namespace Grid.PathMapGrid
{
    public class PathMapGridView : GridView
    {
        [SerializeField] private Transform _cellContainer;
        [SerializeField] private PathMapCell _pathCellPrefab;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        private List<List<PathMapCell>> _cells;
        
        public List<List<PathMapCell>> Cells => _cells;
        
        public void GenerateMap(List<List<int>> paths, List<List<PathCellType>> types)
        {
            ClearContainer(_cellContainer);
            
            var lineCount = types.Count;
            var columnCount = types[0].Count;

            SetGridLayoutGroupValues(columnCount);
            
            var cells = new List<List<PathMapCell>>();
            
            for (int lineIndex = 0; lineIndex < lineCount; lineIndex++)
            {
                var line = new List<PathMapCell>();
                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    var cell = Instantiate(_pathCellPrefab, _cellContainer);
                    cell.Init(types[lineIndex][columnIndex]);
                    line.Add(cell);
                }
                cells.Add(line);
            }

            foreach (var path in paths)
            {
                for (int lineIndex = 1; lineIndex < lineCount; lineIndex++)
                {
                    cells[lineIndex - 1][path[lineIndex - 1]].SetNextCell(cells[lineIndex][path[lineIndex]]);
                }
            }
            
            _cells = cells;
        }

        private void SetGridLayoutGroupValues(int columnCount)
        {
            var screenWidth = Screen.width;
            _gridLayoutGroup.constraintCount = columnCount;
            _gridLayoutGroup.cellSize = Vector2.one * (screenWidth * 0.9f) / (columnCount * 2 - 1);
            _gridLayoutGroup.spacing = Vector2.one * (screenWidth * 0.9f) / (columnCount * 2 - 1);
        }

        private void OnEnable()
        {
            Invoke(nameof(FillMap), 0.1f);
        }
        
        public void FillMap()
        {
            for (var lineIndex = 0; lineIndex < _cells.Count - 1; lineIndex++)
            {
                var line = _cells[lineIndex];
                foreach (var cell in line)
                {
                    if (!cell.HasNextCell)
                        cell.Init(PathCellType.Empty);
                    else
                        cell.RenderPaths();
                }
            }
        }
    }
}