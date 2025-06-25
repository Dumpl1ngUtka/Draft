using System.Collections.Generic;
using System.Linq;
using Grid.Cells;
using Grid.DraftGrid.SelectUnitsMenu;
using Services.GameControlService;
using Services.PanelService;
using Units;
using UnityEngine;

namespace Grid.DraftGrid
{
    public class DraftGridController : GridController
    {
        [SerializeField] private SelectUnitsPanel _selectUnitsPanel;
        
        private DraftGridModel _model;
        private DraftGridView _view;
        private UnitGridCell _draftedCell;
        
        private List<UnitGridCell> _inactiveCells;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void Enter()
        {
            base.Enter();
            _model = new DraftGridModel();
            _view = gameObject.GetComponent<DraftGridView>();
            _view.InstantiateCells();
            SubscribeToCells(_view.Cells.Select(cell => (GridCell)cell).ToList());
            _selectUnitsPanel.Init(this);
            _view.HideSelectMenu();
            _view.InitChemistryObserver(_model.ChemestryInteractor);
        }

        public void SelectMenuFinished(Unit selectedUnit)
        {
            selectedUnit.Position = _draftedCell.Position;
            _model.AddUnit(selectedUnit);
            var unitGridCells = _view.Cells.Find(cell => cell.Position.OwnEquals(_draftedCell.Position));
            unitGridCells.AddUnit(selectedUnit);
            _view.HideSelectMenu();
        }

        protected override void DraggedFromCell(GridCell startDraggingCell, GridCell overCell)
        { 
            var unitOverCell = overCell as UnitGridCell;
            if (_inactiveCells == null || _inactiveCells.Contains(unitOverCell))
                return;
            _view.Visualizer.SetSizeFor(1f, unitOverCell);
        }

        protected override void DraggedToCell(GridCell startDraggingCell, GridCell overCell)
        {
            var unitOverCell = overCell as UnitGridCell;
            if (_inactiveCells == null || _inactiveCells.Contains(unitOverCell))
                return;
            _view.Visualizer.SetSizeFor(1.2f, unitOverCell);
        }

        protected override void DoubleClicked(GridCell cell)
        {
        }

        protected override void HoldBegin(GridCell from)
        {
            var unitCell = (UnitGridCell)from;
            if (unitCell.Unit == null)
                return;

            var noSwichCells = GetNoSwichCells(unitCell);
            _inactiveCells = noSwichCells;
            _view.Visualizer.SetSizeFor(0.5f, noSwichCells);
            _view.Visualizer.SetSizeFor(1.2f, unitCell);
        }
        
        protected override void HoldFinished(GridCell cell)
        { 
            _view.Visualizer.ResetSize();
            _inactiveCells = null;  
        }

        protected override void Clicked(GridCell cell)
        {
            var unitCell = (UnitGridCell)cell;
            if (unitCell.Unit == null)
            {
                var units = _model.GetUnitsForDraft(unitCell.Position.LineIndex);
                _draftedCell = unitCell;
                _view.ShowSelectMenu(units);
            }
            else
                PanelService.Instance.InstantiateUnitInfoPanel(unitCell.Unit);
        }

        protected override void DragFinished(GridCell from, GridCell to)
        {
            var fromUnitCell = (UnitGridCell)from;
            var toUnitCell = (UnitGridCell)to;
            
            if (fromUnitCell ==  null || toUnitCell == null)
                return;
            
            _view.Visualizer.ResetSize();
            SwitchCard(fromUnitCell, toUnitCell);
        }
        
        public void PressFinishButton()
        {
            if (IsAllCellsFilled())
                _model.DraftFinished();
            else
                PanelService.Instance.InstantiateErrorPanel("draft_fill_cells_error");
        }
        
        private bool IsAllCellsFilled()
        {
            var cellCount = _view.GetUnitsCellsByTeam(TeamType.Player).Count;
            var unitsCount = _model.DraftedUnitsCount;
            return cellCount == unitsCount;
        }
        
        private List<UnitGridCell> GetSwichCells(UnitGridCell from)
        {
            var cells = _view.Cells.Where(cell => from.Unit.Class.LineIndexes.Contains(cell.Position.LineIndex)).ToList();
            return cells.Where(cell => (cell.Unit != null && cell.Unit.Class.LineIndexes.Contains(from.Position.LineIndex)) || cell.Unit == null).ToList();
        }
        
        private List<UnitGridCell> GetNoSwichCells(UnitGridCell from)
        {
            var swichCells = GetSwichCells(from);
            return _view.Cells.Where(cell => !swichCells.Contains(cell)).ToList();
        }

        private void SwitchCard(UnitGridCell from, UnitGridCell to)
        {
            if (!GetSwichCells(from).Contains(to))
            {
                PanelService.Instance.InstantiateErrorPanel("line_index_mismatch_error");
                return;
            }
            
            if (from.Position.TeamType != to.Position.TeamType)
            {
                PanelService.Instance.InstantiateErrorPanel("team_index_mismatch_error");
                return;
            }
            
            var toUnit = to.Unit;
            to.AddUnit(from.Unit);
            from.RemoveUnit();
            if (toUnit != null)
                from.AddUnit(toUnit);
        }
        
        //TODO delete later 
        public void QuickFin()
        {
            QuickFinish();
        }
        
        private void QuickFinish()
        {
            foreach (var cell in _view.Cells)
            {
                var unit = _model.GetUnitsForDraft(cell.Position.LineIndex).First();
                unit.Position = cell.Position;
                _model.AddUnit(unit);
            }

            PressFinishButton();
        }
    }
}