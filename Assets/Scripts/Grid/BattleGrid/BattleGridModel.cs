using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Services.GameControlService;
using Services.PanelService;
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
            EnemyAttack();
            _turnInteractor.EndTurn();
            _turnInteractor.StartTurn();
        }
        
        private void EnemyAttack()
        {
            foreach (var enemy in _enemyUnits)
            {
                if (!enemy.Stats.IsReady)
                    continue;

                var ability = enemy.CurrentAbility;
                var target = ability.GetPreferredTarget(enemy, _enemyUnits, _playerUnits);
                if (target == null)
                    continue;
                //Debug.Log(target.Name);
                UseAbility(enemy, target);
            }
        }
        
        public void UseAbility(Unit from, Unit to)
        {
            if (!from.CurrentAbility.IsRightTarget(from, to))
            {
                PanelService.Instance.InstantiateErrorPanel("no_right_target");
                return;
            }
            from.CurrentAbility.UseAbility(from, to, _playerUnits, _enemyUnits);
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

        public List<Unit> GetMaskableUnits(Unit caster, Unit target)
        {
            return caster.CurrentAbility.GetRange(caster, target, _playerUnits, _enemyUnits);
        }

        public List<Unit> DiceAdditionCells(Unit cellUnit)
        {
            return cellUnit.Reaction.GetReactionCells(cellUnit, _playerUnits);
        }

        public List<Unit> GetUnavailableUnits(Unit caster)
        {
            return _playerUnits.Concat(_enemyUnits).Where(unit => 
                !caster.CurrentAbility.IsRightTarget(caster, unit)).ToList();
        }
        
        public List<Unit> GetAvailableUnits(Unit caster)
        {
            return _playerUnits.Concat(_enemyUnits).Where(unit => 
                caster.CurrentAbility.IsRightTarget(caster, unit)).ToList();
        }
    }
}