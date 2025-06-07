using System.Collections.Generic;
using System.Linq;
using Grid.Cells;
using Units;
using UnityEngine;

namespace Battle.Grid
{
    public class TurnInteractor
    {
        private int _turnCount;
        private readonly List<Unit> _playerUnits;
        private readonly List<Unit> _enemyUnits;
        
        public TurnInteractor(List<Unit> playerUnits, List<Unit> enemyUnits)
        {
            _turnCount = 0;
            _playerUnits = playerUnits;
            _enemyUnits = enemyUnits;
        }
        
        public void StartTurn()
        {
            _turnCount++;
            SetReady(_playerUnits);
            SetReady(_enemyUnits);
        }

        public void EndTurn()
        {
            UnitEndTurn(_playerUnits);
            UnitEndTurn(_enemyUnits);
        }

        private void UnitEndTurn(List<Unit> units)
        {
            foreach (var unit in units.Where(unit => unit != null && !unit.Stats.IsDead))
                unit.EndTurn();
        }
        
        private void SetReady(List<Unit> units)
        {
            foreach (var unit in units.Where(unit => unit != null && !unit.Stats.IsDead)) 
                unit.DiceInteractor.SetRandomDicePower();
        }
    }
}