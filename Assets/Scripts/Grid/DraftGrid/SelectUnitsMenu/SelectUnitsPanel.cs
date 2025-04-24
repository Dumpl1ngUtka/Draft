using System.Collections.Generic;
using Battle.Units;
using UnityEngine;

namespace Grid.DraftGrid.SelectUnitsMenu
{
    public class SelectUnitsPanel : MonoBehaviour
    {
        [SerializeField] private SelectUnitsCell[] _selectUnitsCells;
        public SelectUnitsCell SelectedCell { get; private set; }
        
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
    }
}
