using System.Collections.Generic;
using System.Linq;
using Battle.Units;
using Grid.Cells;
using UnityEngine;

namespace Battle.Grid
{
    public class TurnInteractor
    {
        private int _turnCount;
        private List<UnitGridCell> _playerCells;
        private List<UnitGridCell> _enemyCells;
        
        public TurnInteractor(List<UnitGridCell> playerCells, List<UnitGridCell> enemyCells)
        {
            _turnCount = 0;
            _playerCells = playerCells;
            _enemyCells = enemyCells;
        }
        
        public void StartTurn()
        {
            _turnCount++;
            SetReady(_playerCells.Select(x => x.Unit).ToList());
            SetReady(_enemyCells.Select(x => x.Unit).ToList());
        }

        public void EndTurn()
        {
            EnemyAttack();
            UnitEndTurn(_playerCells.Select(x => x.Unit).ToList());
            UnitEndTurn(_enemyCells.Select(x => x.Unit).ToList());
            StartTurn();
        }

        private void UnitEndTurn(List<Unit> units)
        {
            foreach (var unit in units.Where(unit => !unit.IsDead))
                unit.EndTurn();
        }
        
        private void EnemyAttack()
        {
            foreach (var enemy in _enemyCells)
            {
                var unit = enemy.Unit;
                if (unit.IsDead)
                    continue;
                
                var target = unit.CurrentAbility.GetPreferredTarget(_playerCells);
                unit.CurrentAbility.TryUseAbility(enemy, target, _enemyCells, _playerCells);
            }
        }
        
        private void SetReady(List<Unit> units)
        {
            foreach (var unit in units.Where(unit => !unit.IsDead))
            {
                unit.SetRandomDicePower();
                unit.SetReady(true);
            }
        }
    }
}