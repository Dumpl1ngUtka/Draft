using System.Collections.Generic;
using DungeonMap;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Grid.Cells
{
    public class PathMapCell : GridCell
    {
        [SerializeField] private Image _image;
        [SerializeField] private RectTransform _pathPrefab; 
        private List<PathMapCell> _nextCells = new List<PathMapCell>();
        private PathCellType _pathCellType; 
        private RectTransform RectTransform => transform as RectTransform;
        private bool _isEnabled = true;
        
        public GridPosition Position { get; private set; }
        public PathCellType PathCellType => _pathCellType;
        public bool IsEnabled => _isEnabled;

        public void Init(PathCellType type, GridPosition position)
        {
            Position = position;
            _pathCellType = type;
            _image.sprite = Resources.Load<Sprite>("Sprites/PathCellTypes/" + type);
            SetEnable(type != PathCellType.Empty);
        }
        
        public void Init(int type, GridPosition position)
        {
            Init((PathCellType)type, position);
        }
        
        public void SetNextCell(PathMapCell pathMapCell)
        {
            _nextCells.Add(pathMapCell);
        }
        
        public void RenderPaths()
        {
            if (_pathCellType == PathCellType.Empty)
                return;
            
            foreach (var cell in _nextCells)
            {
                var path = Instantiate(_pathPrefab, this.transform);
                var delta = cell.RectTransform.anchoredPosition - RectTransform.anchoredPosition;
                path.localPosition = delta /2 ;
                path.localScale = new Vector3(10, delta.magnitude, 10);
                if (delta.x == 0)
                    continue;
                var rotationDegree = -Mathf.Sign(delta.x) * Mathf.Atan(Mathf.Abs(delta.x / delta.y)) * Mathf.Rad2Deg;
                path.Rotate(Vector3.forward, rotationDegree);
            }
        }

        public void SetEnable(bool isEnable)
        {
            _isEnabled = isEnable;
        }

        public void SetColor(Color color)
        {
            _image.color = color;
        }
    }
}