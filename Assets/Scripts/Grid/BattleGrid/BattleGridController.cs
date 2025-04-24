using System.Collections.Generic;
using Battle.Grid.Visualization;
using Battle.Units;
using Grid.Cells;
using Services.PanelService;

namespace Grid.BattleGrid
{
    public class BattleGridController : GridController
    {
        private BattleGridModel _model;
        private bool _isDragStartSeccess;
        private BattleGridView _view;
        private UnitGridCell _draftedCell;
        
        public override void Init()
        {
            base.Init();
            _view = gameObject.GetComponent<BattleGridView>();
        }

        public void OnTurnButtonClicked()
        {
            _model.EndTurn();
            InteractFinised();
        }
        
        public void Fill(List<Unit> playerUnits, List<Unit> enemyUnits)
        {
            _view.Fill(playerUnits, enemyUnits, out var playerCells, out var enemyCells);
            _model = new BattleGridModel(playerCells, enemyCells);
            _model.StartTurn();
            
            //GridVisualizer.ResetOverPanels();
        }
        
        protected override void DraggedFromCell(GridCell startDraggingCell, GridCell overCell)
        {
            //GridVisualizer.SetSizeFor(1f,startDraggingCell.Unit.CurrentAbility.GetRange(startDraggingCell, overCell, _playerCells, _enemyCells));
        }

        protected override void DraggedToCell(GridCell startDraggingCell, GridCell overCell)
        {
            //GridVisualizer.SetSizeFor(1.1f,startDraggingCell.Unit.CurrentAbility.GetRange(startDraggingCell, overCell, _playerCells, _enemyCells));
        }

        protected override void DoubleClicked(GridCell cell)
        {
            var unitCell = cell as UnitGridCell;
            InteractFinised();
            _model.UseAbility(unitCell, unitCell);
        }

        protected override void HoldFinished(GridCell cell)
        {
            InteractFinised();
        }

        protected override void HoldBegin(GridCell from)
        {
            var cell = from as UnitGridCell;
            
            _isDragStartSeccess = false;
            if (cell.Unit.IsDead)
            {
                PanelService.Instance.InstantiateErrorPanel("unit_is_dead_error");
                return;
            }
            
            if (!cell.Unit.IsReady)
            {
                PanelService.Instance.InstantiateErrorPanel("unit_not_ready_error");
                return;
            }

            if (cell.TeamType != TeamType.Player)
            {
                PanelService.Instance.InstantiateErrorPanel("no_player_unit_error");
                return;
            }
            
            _isDragStartSeccess = true;
            //GridVisualizer.SetOverPanelColor(from, new Color(0.6f, 0,0, 0.4f));
            //GridVisualizer.SetSizeFor(1.1f,from.Unit.CurrentAbility.GetRange(from, from, _playerCells, _enemyCells));
            //GridVisualizer.RenderDiceAdditionValueFor(1, from.Unit.Reaction.GetReactionCells(from, _playerCells));
            //GridVisualizer.RenderHitProbabilityForAll(from);
        }

        protected override void Clicked(GridCell cell)
        {
            InteractFinised();
            PanelService.Instance.InstantiateUnitInfoPanel((cell as UnitGridCell).Unit);
        }

        protected override void DragFinished(GridCell from, GridCell to)
        {
            InteractFinised();
            
            if (!_isDragStartSeccess)
                return;

            _model.UseAbility(from as UnitGridCell, to as UnitGridCell);
        }
        
        private void InteractFinised()
        {
            //GridVisualizer.ResetDiceAdditionValue();
            //GridVisualizer.ResetSize();
            //GridVisualizer.HideOverText();
            //GridVisualizer.ResetOverPanels();

            _model.CheckEndBattle();
        }

    }
}