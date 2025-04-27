using System.Collections.Generic;
using Grid.Cells;
using UnityEngine;

namespace Grid.PathMapGrid
{
    public class PathMapGridView : GridView
    {
        private int ColumnCount = 5;
        private int LineCount = 5;

        [SerializeField] private Color[] _colors;
        [SerializeField] private Transform _cellContainer;
        [SerializeField] private PathMapCell _pathCellPrefab;
        private List<List<PathMapCell>> _cells;
        public List<List<PathMapCell>> GenerateMap()
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
            _cells = cells;
            
            Invoke(nameof(ColoredCells), 1f);
            
            return cells;
        }

        public void ColoredCells()
        {
            var index = 0;
            foreach (var line in _cells)
            {
                //var lineColor = _colors[index++] ;
                foreach (var cell in line)
                {
                    cell.SetColor(cell.HasNextCell ? new Color(1, 1, 1, 1) : new Color(0, 0, 0, 0));
                }
            }
        }
    }
}