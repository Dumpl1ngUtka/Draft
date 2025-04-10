using System.Collections.Generic;
using System.Linq;
using Battle.Units;
using UnityEngine;

namespace Battle.Grid
{
    public class TurnInteractor
    {
        private int _turnCount;
        private List<GridCell> _playerCells;
        private List<GridCell> _enemyCells;
        
        public TurnInteractor(List<GridCell> playerCells, List<GridCell> enemyCells)
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
            foreach (var enemy in _enemyCells)
            {
                var unit = enemy.Unit;
                if (unit.IsDead)
                    continue;
                
                var target = unit.CurrentAbility.GetPreferredTarget(_playerCells);
                unit.CurrentAbility.TryUseAbility(enemy, target, _enemyCells, _playerCells);
            }
            StartTurn();
        }
        
        private void SetReady(List<Unit> units)
        {
            foreach (var unit in units.Where(unit => !unit.IsDead))
            {
                unit.SetRandomPower();
                unit.SetReady(true);
            }
        }
    }
}