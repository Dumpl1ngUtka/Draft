using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Units;
using Battle.Units.Interactors.Reaction;
using Services.PanelService;
using Services.PanelService.Panels;
using UnityEngine;

namespace Battle.Grid
{
    public class BattleGrid : Grid
    {
        [SerializeField] private Transform _playerContainer;
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private CardInfoPanel _cardInfoPrefab;
        private List<GridCell> _playerCells;
        private List<GridCell> _enemyCells;
        private List<Unit> _playerUnits;
        private List<Unit> _enemyUnits;
        
        private TurnInteractor _turnInteractor;
        private UseReactionInteractor _useReactionInteractor;
        
        private bool _isDragStartSeccess = false;
        
        public Action PlayerDefeated;
        public Action PlayerWin;
        
        public void EndTurn()
        {
            InteractFinised();
            _turnInteractor.EndTurn();
        }

        public override void Init()
        {
            base.Init();
            _useReactionInteractor = new UseReactionInteractor();
            _playerCells = InitiateCells(LineCount, ColumnCount, PlayerTeamID, _playerContainer);
            _enemyCells = InitiateCells(LineCount, ColumnCount, EnemyTeamID, _enemyContainer);
            _turnInteractor = new TurnInteractor(_playerCells, _enemyCells);
        }

        public void Fill(List<Unit> playerUnits, List<Unit> enemyUnits)
        {
            FillCells(playerUnits, _playerCells);
            _playerUnits = playerUnits;            
            
            FillCells(enemyUnits, _enemyCells);
            _enemyUnits = enemyUnits;
            
            _turnInteractor.StartTurn();
            GridVisualizer.ResetOverPanels();
            
        }

        private void FillCells(List<Unit> units, List<GridCell> cells)
        {
            var index = 0;
            foreach (var cell in cells)
            {
                cell.AddUnit(units[index++]);
            }
        }

        protected override void HoldBegin(GridCell from)
        {
            _isDragStartSeccess = false;
            if (from.Unit.IsDead)
            {
                PanelService.Instance.InstantiateErrorPanel("unit_is_dead_error");
                return;
            }
            
            if (!from.Unit.IsReady)
            {
                PanelService.Instance.InstantiateErrorPanel("unit_not_ready_error");
                return;
            }

            if (from.TeamIndex != PlayerTeamID)
            {
                PanelService.Instance.InstantiateErrorPanel("no_player_unit_error");
                return;
            }
            
            _isDragStartSeccess = true;
            GridVisualizer.SetOverPanelColor(from, new Color(0.6f, 0,0, 0.4f));
            GridVisualizer.SetSizeFor(1.1f,from.Unit.CurrentAbility.GetRange(from, from, _playerCells, _enemyCells));
            GridVisualizer.RenderDiceAdditionValueFor(1, from.Unit.Reaction.GetReactionCells(from, _playerCells));
            GridVisualizer.RenderHitProbabilityForAll(from);
        }

        protected override void DraggedFromCell(GridCell startDraggingCell, GridCell overCell)
        {
            GridVisualizer.SetSizeFor(1f,startDraggingCell.Unit.CurrentAbility.GetRange(startDraggingCell, overCell, _playerCells, _enemyCells));
        }

        protected override void DraggedToCell(GridCell startDraggingCell, GridCell overCell)
        {
            GridVisualizer.SetSizeFor(1.1f,startDraggingCell.Unit.CurrentAbility.GetRange(startDraggingCell, overCell, _playerCells, _enemyCells));
        }

        protected override void DoubleClicked(GridCell cell)
        {
            InteractFinised();
            UseAbility(cell, cell);
        }

        protected override void HoldFinished(GridCell from)
        {
            InteractFinised();
        }

        protected override void Clicked(GridCell cell)
        {
            InteractFinised();
            PanelService.Instance.InstantiateCardInfoPanel(cell.Unit);
        }

        protected override void DragFinished(GridCell from, GridCell to)
        {
            InteractFinised();
            
            if (!_isDragStartSeccess)
                return;

            UseAbility(from, to);
        }

        private void UseAbility(GridCell from, GridCell to)
        {
            var response = from.Unit.CurrentAbility.TryUseAbility(from, to, _playerCells, _enemyCells);
            if (response.Success)
            {
                from.Unit.SetReady(false);
            }
            else
            {
                PanelService.Instance.InstantiateErrorPanel(response.Message);
                return;
            }
            
            response = _useReactionInteractor.UseReaction(from, _playerCells);
            if (!response.Success)
            {
                PanelService.Instance.InstantiateErrorPanel(response.Message);
                return;
            }
        }

        private void InteractFinised()
        {
            GridVisualizer.ResetDiceAdditionValue();
            GridVisualizer.ResetSize();
            GridVisualizer.HideOverText();
            GridVisualizer.ResetOverPanels();

            CheckEndBattle();
        }

        private void CheckEndBattle()
        {
            HasAliveUnits(_playerUnits, PlayerDefeated);
            HasAliveUnits(_enemyUnits, PlayerWin);
        }

        private void HasAliveUnits(List<Unit> units, Action callback)
        {
            var aliveUnitsCount =  units.Count(x => !x.IsDead);
            if (aliveUnitsCount == 0)
                callback?.Invoke();
        }
    }
}