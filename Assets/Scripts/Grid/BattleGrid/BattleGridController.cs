using System.Linq;
using Grid.Cells;
using Services.GameControlService;
using Services.PanelService;
using UnityEngine;

namespace Grid.BattleGrid
{
    public class BattleGridController : GridController
    {
        private BattleGridModel _model;
        private BattleGridView _view;
        private bool _isDragStartSeccess = false;
        private UnitGridCell _startInteractedCell;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void Enter()
        {
            base.Enter();
            _model = new BattleGridModel();
            _view = gameObject.GetComponent<BattleGridView>();
            _view.InitiateUnitCells();
            
            Fill();

             _view.GetCells(out var playerCells, out var enemyCells);
            SubscribeToCells(playerCells.Select(x => x as GridCell).ToList());
            SubscribeToCells(enemyCells.Where(x => x.IsActive).Select(x => x as GridCell).ToList());
            
            _model.StartTurn();
            InteractFinised();
        }

        public override void Exit()
        {
            base.Exit();
            _model.GetUnits(out var playerUnits, out var _);
            GameControlService.Instance.CurrentRunInfo.SavePlayerUnits(playerUnits);
        }

        public void OnTurnButtonClicked()
        {
            _model.EndTurn();
            InteractFinised();
        }
        
        private void Fill()
        {
            _view.GetCells(out var playerCells, out var enemyCells);
            _model.GetUnits(out var playerUnits, out var enemyUnits);

            foreach (var unit in playerUnits)
            {
                var cell = playerCells.Find(cell => cell.Position.OwnEquals(unit.Position));
                Debug.Log(unit);
                cell.AddUnit(unit);
            }

            for (int i = 0; i < enemyCells.Count; i++)
            {
                var unit = enemyUnits[i];
                var cell = enemyCells[i];
                if (unit == null)
                {
                    cell.Deactivate();
                }
                else
                {
                    unit.Position = cell.Position;
                    cell.AddUnit(unit);
                }
            }
        }
        
        protected override void DraggedFromCell(GridCell startDraggingCell, GridCell overCell)
        {
            _view.ResetSize();
        }

        protected override void DraggedToCell(GridCell startDraggingCell, GridCell overCell)
        {
            var unitOverCell = overCell as UnitGridCell;
            var unitStartDraggingCell = startDraggingCell as UnitGridCell;
            
            if (unitOverCell == null || unitStartDraggingCell == null) return;
            if (startDraggingCell != overCell)
                _view.SetSizeFor(1.1f, _model.GetMaskableUnits(unitStartDraggingCell.Unit, unitOverCell.Unit));
        }

        protected override void DoubleClicked(GridCell cell)
        {
            var unitCell = cell as UnitGridCell;
            if (unitCell == null) return;
            
            InteractFinised();
            
            if (unitCell.Unit.Stats.IsDead)
            {
                PanelService.Instance.InstantiateErrorPanel("unit_is_dead_error");
                return;
            }
            
            if (!unitCell.Unit.Stats.IsReady)
            {
                PanelService.Instance.InstantiateErrorPanel("unit_not_ready_error");
                return;
            }

            if (unitCell.Position.TeamType != TeamType.Player)
            {
                PanelService.Instance.InstantiateErrorPanel("no_player_unit_error");
                return;
            }
            
            _model.UseAbility(unitCell.Unit, unitCell.Unit);
        }

        protected override void HoldFinished(GridCell cell)
        {
            InteractFinised();
        }

        protected override void HoldBegin(GridCell from)
        {
            var cell = from as UnitGridCell;
            _startInteractedCell = cell;
            if (cell == null) return;
            
            if (cell.Unit.Stats.IsDead)
            {
                PanelService.Instance.InstantiateErrorPanel("unit_is_dead_error");
                return;
            }
            
            if (!cell.Unit.Stats.IsReady)
            {
                PanelService.Instance.InstantiateErrorPanel("unit_not_ready_error");
                return;
            }

            if (cell.Position.TeamType != TeamType.Player)
            {
                PanelService.Instance.InstantiateErrorPanel("no_player_unit_error");
                return;
            }
            
            _isDragStartSeccess = true;
            _view.SetSpriteColorFor(_model.GetUnavailableUnits(cell.Unit), new Color(0f, 0,0, 0.4f));
            _view.SetSizeFor(1.1f, _model.GetMaskableUnits(cell.Unit, cell.Unit));
            _view.SetSpriteColorFor(cell.Unit, Color.red);
            _view.SetDiceAdditionValue(_model.DiceAdditionCells(cell.Unit), 1);
            //GridVisualizer.RenderHitProbabilityForAll(from);
        }

        protected override void Clicked(GridCell cell)
        {
            InteractFinised();
            PanelService.Instance.InstantiateUnitInfoPanel((cell as UnitGridCell)?.Unit);
        }

        protected override void DragFinished(GridCell from, GridCell to)
        {
            InteractFinised();
            var fromUnitCell = from as UnitGridCell;
            var toUnitCell = to as UnitGridCell;

            if (!_isDragStartSeccess)
                return;
            _isDragStartSeccess = false;
            
            _model.UseAbility(fromUnitCell?.Unit, toUnitCell?.Unit);
        }
        
        private void InteractFinised()
        {
            //GridVisualizer.HideOverText();
            
            _view.ResetSize();
            _view.ResetSpriteColor();
            _model.CheckEndBattle();
            _view.ResetDiceAdditionValue();
        }
    }
}