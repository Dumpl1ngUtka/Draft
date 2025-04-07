using System;
using System.Collections.Generic;
using System.Linq;
using Battle.InfoPanel;
using Battle.Units;
using Battle.Units.Interactors.Ability;
using Battle.Units.Interactors.Reaction;
using UnityEngine;
using Random = UnityEngine.Random;

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
        
        private UseAbilityInteractor _useAbilityInteractor;
        private UseReactionInteractor _useReactionInteractor;
        
        public override void Init()
        {
            _useAbilityInteractor = new UseAbilityInteractor();
            _useReactionInteractor = new UseReactionInteractor();
            _playerCells = InitiateCells(LineCount, ColumnCount, PlayerTeamID, _playerContainer);
            _enemyCells = InitiateCells(LineCount, ColumnCount, EnemyTeamID, _enemyContainer);
        }

        public void Fill(List<Unit> playerUnits, List<Unit> enemyUnits)
        {
            FillCells(playerUnits, _playerCells);
            _playerUnits = playerUnits;            
            
            FillCells(enemyUnits, _enemyCells);
            _enemyUnits = enemyUnits;
            
            NewTurn();
        }

        private void FillCells(List<Unit> units, List<GridCell> cells)
        {
            var index = 0;
            foreach (var cell in cells)
            {
                cell.AddUnit(units[index++]);
            }
        }

        public void EndTurn()
        {
            foreach (var enemy in _enemyUnits)
            {
                if (enemy.IsDead)
                    return;
                
                var target = _useAbilityInteractor.GetPreferredTarget(0, enemy, _playerUnits);
                _useAbilityInteractor.UseAbility(enemy.DicePower, enemy, target, _enemyUnits, _playerUnits);
            }
            NewTurn();
        }
        
        public void NewTurn()
        {
            SetReady(_playerUnits);
            SetReady(_enemyUnits);
        }

        private void SetReady(List<Unit> units)
        {
            foreach (var unit in units.Where(unit => !unit.IsDead))
            {
                unit.SetReady(true);
                unit.SetRandomPower();
            }
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

        protected override void Dragged(GridCell from, GridCell to)
        {
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
            
            var response = _useAbilityInteractor.UseAbility(0, from.Unit, to.Unit, _playerUnits, _enemyUnits);
            if (!response.Success)
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