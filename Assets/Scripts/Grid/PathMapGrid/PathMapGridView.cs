using System;
using System.Collections.Generic;
using DungeonMap;
using Grid.Cells;
using Units;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Grid.PathMapGrid
{
    public class PathMapGridView : GridView
    {
        [SerializeField] private Transform _cellContainer;
        [SerializeField] private PathMapCell _pathCellPrefab;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        [Header("Color")]
        [SerializeField] private Color _disableColor;
        [SerializeField] private Color _enableColor;
        [SerializeField] private Color _visitedColor;
        private List<List<PathMapCell>> _cells;
        
        public List<List<PathMapCell>> Cells => _cells;
        
        public void GenerateMap(int[,] map, int[] path)
        {
            ClearContainer(_cellContainer);
            
            var lineCount = map.GetLength(0);
            var columnCount = map.GetLength(1);

            SetGridLayoutGroupValues(columnCount);
            
            var cells = new List<List<PathMapCell>>();
            
            for (int lineIndex = 0; lineIndex < lineCount; lineIndex++)
            {
                var line = new List<PathMapCell>();
                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    var cell = Instantiate(_pathCellPrefab, _cellContainer);
                    cell.Init(map[lineIndex, columnIndex], new GridPosition(lineIndex, columnIndex, TeamType.Player));
                    line.Add(cell);
                    MarkCell(cell, path);
                }
                cells.Add(line);
            }
            _cells = cells;
        }

        private void MarkCell(PathMapCell cell, int[] path)
        {
            cell.SetEnable(false);
            cell.SetColor(_enableColor);
            var lineIndex = cell.Position.LineIndex;
            var columnIndex = cell.Position.ColumnIndex;
            
            if (lineIndex < path.Length)
            {
                cell.SetColor(_disableColor);
                cell.SetEnable(false);
                if (path.Length >= lineIndex && path[lineIndex] == columnIndex)
                    cell.SetColor(_visitedColor);
            }
            else if (lineIndex == path.Length)
            {
                cell.SetEnable(true);
                cell.SetColor(_enableColor);
                
                if (path.Length != 0 && Math.Abs(path[^1] - columnIndex) > 1)
                {
                    cell.SetEnable(false);
                    cell.SetColor(_disableColor);
                }
            }
        }

        private void SetGridLayoutGroupValues(int columnCount)
        {
            var screenWidth = Screen.width;
            _gridLayoutGroup.constraintCount = columnCount;
            _gridLayoutGroup.cellSize = Vector2.one * (screenWidth * 0.9f) / (columnCount * 2 - 1);
            _gridLayoutGroup.spacing = Vector2.one * (screenWidth * 0.9f) / (columnCount * 2 - 1);
        }
    }
}