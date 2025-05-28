using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Services.GameControlService;
using Units;
using UnityEngine;

namespace Grid.BattleGrid
{
    public class BattleGridModel : GridModel
    {
        private List<Unit> _playerUnits;
        private List<Unit> _enemyUnits;        
        private TurnInteractor _turnInteractor;

        public BattleGridModel()
        {
            _playerUnits = GameControlService.CurrentRunInfo.PlayerUnits.ToList();
            foreach (var unit in _playerUnits)
            {
                Debug.Log(unit);
            }
            _enemyUnits = GenerateEnemies();
            
            _turnInteractor = new TurnInteractor(_playerUnits, _enemyUnits);
        }
        
        private List<Unit> GenerateEnemies()
        {
            var preset = GameControlService.Instance.CurrentDungeonInfo.GetEnemyPositionPreset();
            return preset.GetUnitPresets().
                Select(unitPreset => unitPreset == null ? null : new Unit(unitPreset)).ToList();
        }

        public void EndTurn()
        {
            _turnInteractor.EndTurn();
            //EnemyAttack();

        }
        
        private void EnemyAttack()
        {
            foreach (var enemy in _enemyUnits)
            {
                if (enemy.Stats.IsDead)
                    continue;

                var ability = enemy.CurrentAbility;
                var target = ability.GetPreferredTarget(_playerUnits);
                var range = ability.GetRange(enemy, target, _enemyUnits, _playerUnits);
                foreach (var unit in range)
                {
                    if (!ability.IsRightTarget(enemy, unit)) continue;
                    if (unit.Stats.IsDead || !unit.IsReady) continue;
                    
                    ability.UseAbility(enemy, unit, _enemyUnits, _playerUnits);
                }
            }
        }
        
        public void UseAbility(Unit from, Unit to)
        {
            from.CurrentAbility.UseAbility(from, to, _playerUnits, _enemyUnits);
            from.SetReady(false);
            from.Reaction.UseReaction(from, _playerUnits);
        }
        
        public void CheckEndBattle()
        {
            if (!HasAliveUnits(_enemyUnits))
            {
                GameControlService.ChangeGrid(GameControlService.PathMapGridPrefab);
                GameControlService.Instance.CurrentRunInfo.SavePlayerUnits(_playerUnits);
            }
            else if (!HasAliveUnits(_playerUnits))
            {
                GameControlService.ChangeGrid(GameControlService.SelectDungeonGridPrefab);
                //calculateRes
            }
        }

        private bool HasAliveUnits(List<Unit> units)
        {
            return units.Any(unit => !unit.Stats.IsDead);
        }

        public void StartTurn()
        {
            _turnInteractor.StartTurn();
        }

        public void GetUnits(out List<Unit> playerUnits, out List<Unit> enemyUnits)
        {
            playerUnits = _playerUnits;
            enemyUnits = _enemyUnits;
        }
    }
}