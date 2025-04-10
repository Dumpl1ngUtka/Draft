using System;
using System.Collections.Generic;
using System.Linq;
using Battle.InfoPanel;
using Battle.PassiveEffects;
using Battle.Units;
using Battle.Units.Interactors.Reaction;
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
        private GridVisualizer _gridVisualizer;
        private UseReactionInteractor _useReactionInteractor;
        
        private bool _isDragStartSeccess = false;
        
        public void EndTurn() => _turnInteractor.EndTurn();
        
        public override void Init()
        {

            _useReactionInteractor = new UseReactionInteractor();
            _playerCells = InitiateCells(LineCount, ColumnCount, PlayerTeamID, _playerContainer);
            _enemyCells = InitiateCells(LineCount, ColumnCount, EnemyTeamID, _enemyContainer);
            _gridVisualizer = new GridVisualizer(_playerCells, _enemyCells);
            _turnInteractor = new TurnInteractor(_playerCells, _enemyCells);
        }

        public void Fill(List<Unit> playerUnits, List<Unit> enemyUnits)
        {
            FillCells(playerUnits, _playerCells);
            _playerUnits = playerUnits;            
            
            FillCells(enemyUnits, _enemyCells);
            _enemyUnits = enemyUnits;
            
            _turnInteractor.StartTurn();
        }

        private void FillCells(List<Unit> units, List<GridCell> cells)
        {
            var index = 0;
            foreach (var cell in cells)
            {
                cell.AddUnit(units[index++]);
            }
        }
        
        protected override void DragOverCell(GridCell from, GridCell over)
        {
            _gridVisualizer.ResetSize();
            _gridVisualizer.SetSizeFor(1.1f,from.Unit.CurrentAbility.GetRange(from, over, _playerCells, _enemyCells));
        }

        protected override void HoldBegin(GridCell from)
        {
            _isDragStartSeccess = false;
            if (!from.Unit.IsReady)
            {
                InstantiateErrorPanel("unit_not_ready_error");
                return;
            }

            if (from.TeamIndex != PlayerTeamID)
            {
                InstantiateErrorPanel("no_player_unit_error");
                return;
            }
            
            _isDragStartSeccess = true;
            _gridVisualizer.RenderDiceAdditionValueFor(1, from.Unit.Reaction.GetReactionCells(from, _playerCells));
            _gridVisualizer.RenderHitProbability(from);
        }
        
        protected override void HoldFinished(GridCell from)
        {
            _gridVisualizer.ResetDiceAdditionValue();
            _gridVisualizer.ResetSize();
            _gridVisualizer.HideOverText();
        }

        protected override void Clicked(GridCell cell)
        {
            ShowCardInfo(cell.Unit);
        }

        private void ShowCardInfo(Unit cellUnit)
        {
            _cardInfoPrefab.Instantiate(cellUnit);
            _cardInfoPrefab.Render(cellUnit);
        }

        protected override void DragFinished(GridCell from, GridCell to)
        {
            _gridVisualizer.ResetDiceAdditionValue();
            _gridVisualizer.ResetSize();
            _gridVisualizer.HideOverText();
            
            if (!_isDragStartSeccess)
                return;
            
            var response = from.Unit.CurrentAbility.TryUseAbility(from, to, _playerCells, _enemyCells);
            if (response.Success)
            {
                from.Unit.SetReady(false);
            }
            else
            {
                InstantiateErrorPanel(response.Message);
                return;
            }
            
            response = _useReactionInteractor.UseReaction(from, _playerCells);
            if (!response.Success)
            {
                InstantiateErrorPanel(response.Message);
                return;
            }
        }
    }
}