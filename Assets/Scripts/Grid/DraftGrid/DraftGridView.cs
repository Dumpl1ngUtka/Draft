using System.Collections.Generic;
using Battle.Grid.Visualization;
using Grid.Cells;
using Grid.DraftGrid.ChemistryBoard;
using Grid.GridEffects.UnitGridCellEffects;
using Units;
using UnityEngine;

namespace Grid.DraftGrid
{
    public class DraftGridView : GridView
    {
        [SerializeField] private SelectUnitsMenu.SelectUnitsPanel _selectUnitsPanel;
        [SerializeField] private ChemistryObserver _chemistryObserver;
        public List<UnitGridCell> Cells { get; private set; }
        
        public void InstantiateCells()
        {
            Cells = InitiateUnitCells();
        }
        
        public void InitChemistryObserver(CustomObserver.IObservable<ChemestryInteractor> observable)
        {
            _chemistryObserver.Init(observable);
        }

        public void ShowSelectMenu(List<Unit> units)
        {
            _selectUnitsPanel.SetActive(true);
            _selectUnitsPanel.RenderUnits(units);
        }
        
        public void HideSelectMenu()
        {
            _selectUnitsPanel.SetActive(false);
        }
    }
}