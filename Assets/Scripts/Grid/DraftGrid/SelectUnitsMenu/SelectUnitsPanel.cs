using System.Collections.Generic;
using Battle.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Grid.DraftGrid.SelectUnitsMenu
{
    public class SelectUnitsPanel : MonoBehaviour
    {
        [SerializeField] private SelectUnitsCell[] _selectUnitsCells;
        [SerializeField] private GridLayoutGroup _container;
        public SelectUnitsCell SelectedCell { get; private set; }
        
        public void Init()
        {
            ControlSize();
        }
        
        public void SetActive(bool isActive)
        {
            SelectedCell = null;
            SelectUnitCell(SelectedCell);
            gameObject.SetActive(isActive);
        }

        public void RenderUnits(List<Unit> units)
        {
            var i = 0;
            foreach (var cell in _selectUnitsCells)
                cell.Init(this, units[i++]);
        }

        public void SelectUnitCell(SelectUnitsCell selectedCell)
        {
            foreach (var cell in _selectUnitsCells)
            {
                cell.SetOutline(false);
            }
            selectedCell?.SetOutline(true);
            SelectedCell = selectedCell;
        }
        
        private void ControlSize()
        {
            var containerWidth = ((RectTransform)_container.transform).rect.width;
            var sidesRatio = _container.cellSize / (Mathf.Min(_container.cellSize.x, _container.cellSize.y));
            _container.cellSize = sidesRatio * (containerWidth * 0.9f) / 3;    
        }
    }
}
