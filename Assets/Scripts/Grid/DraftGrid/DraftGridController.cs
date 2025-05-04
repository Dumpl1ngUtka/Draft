using System.Collections.Generic;
using Grid.Cells;
using Grid.DraftGrid.SelectUnitsMenu;
using Services.GameControlService.GridStateMachine;
using Services.PanelService;
using UnityEngine;

namespace Grid.DraftGrid
{
    public class DraftGridController : GridController
    {
        [SerializeField] private SelectUnitsPanel _selectUnitsPanel;
        
        private DraftGridModel _model;
        private DraftGridView _view;
        private bool _isDragBeginSuccess;
        private UnitGridCell _draftedCell;
        
        private List<UnitGridCell> _inactiveCells;


        public override void Init(GridStateMachine gridStateMachine)
        {
            base.Init(gridStateMachine);
            _view = gameObject.GetComponent<DraftGridView>();
            _model = new DraftGridModel(gridStateMachine, _view.GetUnitsCellsByTeam(TeamType.Player));
            _selectUnitsPanel.Init();
        }

        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public void OnSelectMenuFinished()
        {
            var selectedCell = _selectUnitsPanel.SelectedCell;
            if (selectedCell == null)
            {
                PanelService.Instance.InstantiateErrorPanel("select_cell_empty_error");
            }
            else
            {
                _model.SetUnitToCell(selectedCell.Unit, _draftedCell);
                _view.HideSelectMenu();
            }
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
            _isDragBeginSuccess = false;
            if (unitCell.Unit == null)
            {
                //InstantiateErrorPanel("unit_null_error");
                return;
            }

            var noSwichCells = _model.GetNoSwichCells(unitCell);
            _inactiveCells = noSwichCells;
            _view.Visualizer.SetSizeFor(0.5f, noSwichCells);
            _isDragBeginSuccess = true;
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
                var units = _model.GetUnitsForDraft(unitCell);
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
            
            if (!_isDragBeginSuccess)
                return;
            
            _view.Visualizer.ResetSize();
            _model.SwitchCard(fromUnitCell, toUnitCell);
        }
        
        public void PressFinishButton()
        {
            _model.TryFinish();
        }
        
        public void QuickFin()
        {
            _model.QuickFinish();
        }
    }
}