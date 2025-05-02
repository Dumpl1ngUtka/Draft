using System;
using System.Collections.Generic;
using Grid.Cells;
using UnityEngine;

namespace Grid.PathMapGrid
{
    public class PathMapGridView : GridView
    {
        private int ColumnCount = 5;
        private int LineCount = 5;

        [SerializeField] private Transform _cellContainer;
        [SerializeField] private PathMapCell _pathCellPrefab;
        private List<List<PathMapCell>> _cells;
        
        public List<List<PathMapCell>> Cells => _cells;
        
        public void GenerateEmptyMap(List<List<int>> paths)
        {
            ClearContainer(_cellContainer);
            
            var cells = new List<List<PathMapCell>>();
            
            for (int lineIndex = 0; lineIndex < LineCount; lineIndex++)
            {
                var line = new List<PathMapCell>();
                for (int columnCount = 0; columnCount < ColumnCount; columnCount++)
                {
                    var cell = Instantiate(_pathCellPrefab, _cellContainer); 
                    line.Add(cell);
                }
                cells.Add(line);
            }

            for (int pathIndex = 0; pathIndex < paths.Count; pathIndex++)
            {
                var path = paths[pathIndex];
                for (int lineIndex = 1; lineIndex < LineCount; lineIndex++)
                {
                    cells[lineIndex - 1][path[lineIndex - 1]].SetNextCell(cells[lineIndex][path[lineIndex]]);
                }
            }
            
            _cells = cells;
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
                for (var cellInLineIndex = 0; cellInLineIndex < line.Count; cellInLineIndex++)
                {
                    var cell = line[cellInLineIndex];
                    cell.SetColor(cell.HasNextCell ? new Color(1, 1, 1, 1) : new Color(0, 0, 0, 0));
                }
            }
            var lastLine = _cells[^1];
            for (var cellInLineIndex = 0; cellInLineIndex < lastLine.Count; cellInLineIndex++)
            {
                var cell = lastLine[cellInLineIndex];
                cell.SetColor(new Color(1, 1, 1, 1));
            }
        }
    }
}