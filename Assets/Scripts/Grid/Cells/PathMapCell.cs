using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grid.Cells
{
    public class PathMapCell : GridCell
    {
        [SerializeField] private LineRenderer _line;
        [SerializeField] private Image _image; 
        private List<PathMapCell> _nextCells = new List<PathMapCell>();
        public bool HasNextCell => _nextCells.Count > 0;
        public RectTransform RectTransform => _line.transform as RectTransform;
        
        public void SetNextCell(PathMapCell pathMapCell)
        {
            _nextCells.Add(pathMapCell);
        }

        public void SetColor(Color color)
        {
            _image.color = color;
            if (color != Color.clear)
            {
                
            }
        }
    }
}