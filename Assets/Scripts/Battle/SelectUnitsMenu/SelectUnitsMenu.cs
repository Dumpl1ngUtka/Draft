using Battle.Grid;
using Battle.Units;
using UnityEngine;

namespace Battle.SelectUnitsMenu
{
    public class SelectUnitsMenu : MonoBehaviour
    {
        [SerializeField] private SelectUnitsCell[] _selectUnitsCells;
        private SelectUnitsCell _selectedCell;
        private Grid.DraftGrid _grid;
        private GridCell _targetCell;
        
        public void Init(Grid.DraftGrid grid, GridCell cell)
        {
            _grid = grid;
            _targetCell = cell;
        }
        
        public void Enable()
        {
            _selectedCell = null;
            SelectUnitCell(_selectedCell);
            gameObject.SetActive(true);
        }

        public void GenerateCards(Class[] classes)
        {
            foreach (var cell in _selectUnitsCells)
            {
                var preset = UnitPreset.Generate(classes[Random.Range(0, classes.Length)]);
                var unit = new Unit(preset);
                cell.Init(this, unit);
            }
        }

        public void SelectUnitCell(SelectUnitsCell selectedCell)
        {
            foreach (var cell in _selectUnitsCells)
            {
                cell.SetOutline(false);
            }
            selectedCell?.SetOutline(true);
            _selectedCell = selectedCell;
        }
        
        public void ApplyUnit()
        {
            if (_selectedCell == null)
                return;
            
            _grid.AddCard(_targetCell, _selectedCell.Unit);
            Disable();
        }
        
        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
