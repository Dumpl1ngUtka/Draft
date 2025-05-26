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
        private bool _isDragStartSeccess;
        private UnitGridCell _draftedCell;

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
            //_view.Visualizer.ResetOverPanels();
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
            //_view.Visualizer.SetSizeFor(1f,startDraggingCell.Unit.CurrentAbility.GetRange(startDraggingCell, overCell, _playerCells, _enemyCells));
        }

        protected override void DraggedToCell(GridCell startDraggingCell, GridCell overCell)
        {
            //_view.Visualizer.SetSizeFor(1.1f,startDraggingCell.Unit.CurrentAbility.GetRange(startDraggingCell, overCell, _playerCells, _enemyCells));
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
            
            if (!unitCell.Unit.IsReady)
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
            if (cell == null) return;
            
            _isDragStartSeccess = false;
            
            if (cell.Unit.Stats.IsDead)
            {
                PanelService.Instance.InstantiateErrorPanel("unit_is_dead_error");
                return;
            }
            
            if (!cell.Unit.IsReady)
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
            //GridVisualizer.SetOverPanelColor(from, new Color(0.6f, 0,0, 0.4f));
            //GridVisualizer.SetSizeFor(1.1f,from.Unit.CurrentAbility.GetRange(from, from, _playerCells, _enemyCells));
            //GridVisualizer.RenderDiceAdditionValueFor(1, from.Unit.Reaction.GetReactionCells(from, _playerCells));
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

            _model.UseAbility(fromUnitCell?.Unit, toUnitCell?.Unit);
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