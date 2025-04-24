using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Units;
using Services.PanelService;
using Services.PanelService.Panels;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Battle.Grid
{
    /*public class DraftGrid : Grid
    {
        [Header("Draft grid settings")]
        [SerializeField] private Class[] _availableClasses;
        [SerializeField] private Race[] _availableRaces;
        [SerializeField] private SelectUnitsMenu.SelectUnitsMenu _selectUnitsMenu;
        [SerializeField] private CardInfoPanel _cardInfoPrefab;
        [SerializeField] private Transform _container;
        private List<GridCell> _cells;
        private bool _isDragBeginSuccess;
        
        private ChemestryInteractor _chemestryInteractor;
        
        public Action DraftFinished;
        public Action<Unit> UnitAdded;

        public override void Init()
        {
            base.Init();
            _cells = InitiateCells(LineCount, ColumnCount, PlayerTeamID, _container);
            _chemestryInteractor = new ChemestryInteractor(_cells);
            UnitAdded += _chemestryInteractor.UnitAdded; 
        }

        protected override void HoldBegin(GridCell from)
        {
            _isDragBeginSuccess = false;
            if (from.Unit == null)
            {
                //InstantiateErrorPanel("unit_null_error");
                return;
            }

            foreach (var cell in GetNoSwichCells(from))
            {
                GridVisualizer.SetOverPanelColor(cell, Color.red);
            }
            _isDragBeginSuccess = true;
        }

        protected override void DraggedFromCell(GridCell startDraggingCell, GridCell overCell)
        {
            GridVisualizer.SetSizeFor(1f, overCell);
        }

        protected override void DraggedToCell(GridCell startDraggingCell, GridCell overCell)
        {
            GridVisualizer.SetSizeFor(1.2f, overCell);
        }

        protected override void DoubleClicked(GridCell cell)
        {
            
        }

        protected override void HoldFinished(GridCell from)
        {
            GridVisualizer.ResetSize();
        }

        private List<GridCell> GetSwichCells(GridCell from)
        {
            var cells = _cells.Where(cell => from.Unit.Class.LineIndexes.Contains(cell.LineIndex)).ToList();
            return cells.Where(cell => (cell.Unit != null && cell.Unit.Class.LineIndexes.Contains(from.LineIndex)) || cell.Unit == null).ToList();
        }
        
        private List<GridCell> GetNoSwichCells(GridCell from)
        {
            var cells = _cells.Where(cell => !from.Unit.Class.LineIndexes.Contains(cell.LineIndex)).ToList();
            return cells.Where(cell => (cell.Unit != null && cell.Unit.Class.LineIndexes.Contains(from.LineIndex)) || cell.Unit == null).ToList();
        }

        protected override void Clicked(GridCell cell)
        {
            if (cell.Unit == null)
                ShowUnitSelectMenu(cell);
            else 
                PanelService.Instance.InstantiateCardInfoPanel(cell.Unit);
        }

        protected override void DragFinished(GridCell from, GridCell to)
        {
            if (!_isDragBeginSuccess)
                return;
            
            GridVisualizer.ResetSize();
            SwitchCard(from, to);
        }

        private void ShowUnitSelectMenu(GridCell cell)
        {
            _selectUnitsMenu.Init(this, cell);
            var availableClasses = _availableClasses.Where(x => x.LineIndexes.Contains(cell.LineIndex)).ToArray();
            _selectUnitsMenu.Enable();
            _selectUnitsMenu.RenderUnits(availableClasses);
        }
        
        public void AddCard(GridCell cell, Unit unit)
        {
            cell.AddUnit(unit);
            UnitAdded?.Invoke(unit); 
        }

        private void SwitchCard(GridCell from, GridCell to)
        {
            if (!GetSwichCells(from).Contains(to))
            {
                PanelService.Instance.InstantiateErrorPanel("line_index_mismatch_error");
                return;
            }
            
            if (from.TeamIndex != to.TeamIndex)
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

        public List<Unit> GetUnits()
        {
            return _cells.Select(cell => cell.Unit).ToList();
        }

        public void QuickFin()
        {
            foreach (var cell in _cells)
            {
                var unit = new Unit(UnitPreset.Generate(_availableClasses[Random.Range(0, _availableClasses.Length)]));
                cell.AddUnit(unit);
            }
            
            DraftFinished?.Invoke();
        }
        
        public void PressFinishButton()
        {
            var fillCellsCount = _cells.Count(x => x.Unit != null);
            if (fillCellsCount < _cells.Count)
            {
                PanelService.Instance.InstantiateErrorPanel("draft_fill_cells_error");
                return;
            }
            
            DraftFinished?.Invoke();
        }
    }*/
}