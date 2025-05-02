using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grid.Cells
{
    public class PathMapCell : GridCell
    {
        [SerializeField] private LineRenderer _line;
        [SerializeField] private Image _image;
        [SerializeField] private RectTransform _pathPrefab; 
        private List<PathMapCell> _nextCells = new List<PathMapCell>();
        public bool HasNextCell => _nextCells.Count > 0;
        public RectTransform RectTransform => _line.transform as RectTransform;

        public void Init()
        {
            
        }
        
        public void SetNextCell(PathMapCell pathMapCell)
        {
            _nextCells.Add(pathMapCell);
        }

        public void SetColor(Color color)
        {
            _image.color = color;
            if (color != Color.clear)
            {
                foreach (var cell in _nextCells)
                {
                    var path = Instantiate(_pathPrefab, this.transform);
                    var delta = cell.RectTransform.anchoredPosition - RectTransform.anchoredPosition;
                    path.localPosition = delta /2 ;
                    path.localScale = new Vector3(10, delta.magnitude, 10);
                    if (delta.x == 0)
                        continue;
                    Debug.Log(delta.x + " / " + delta.y);
                    var rotationDegree = -Mathf.Sign(delta.x) * Mathf.Atan(Mathf.Abs(delta.x / delta.y)) * Mathf.Rad2Deg;
                    path.Rotate(Vector3.forward, rotationDegree);
                }

            }
        }
    }
}