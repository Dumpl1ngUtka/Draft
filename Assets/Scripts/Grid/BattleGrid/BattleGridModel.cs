using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using DungeonMap;
using Services.PanelService;
using Services.SaveLoadSystem;
using Units;
using UnityEngine;

namespace Grid.BattleGrid
{
    public class BattleGridModel : GridModel
    {
        private RunData _runData;
        private List<Unit> _playerUnits;
        private List<Unit> _enemyUnits;        
        private TurnInteractor _turnInteractor;

        public BattleGridModel()
        {
            _runData = SaveLoadService.Instance.LoadRunData();
            _playerUnits = _runData.GetPlayerUnits().ToList();
            _enemyUnits = GenerateEnemies();
            
            _turnInteractor = new TurnInteractor(_playerUnits, _enemyUnits);
        }
        
        private List<Unit> GenerateEnemies()
        {
            var lineIndex = _runData.GetPath().Length;
            var dungeonInfo = DungeonInfo.GetObjectByID(_runData.DungeonID);
            var presetsList = dungeonInfo.GetEnemyPositionPresets()
                .Where(preset => preset.IsValidLineIndex(lineIndex)).ToList();
            return presetsList[Random.Range(0, presetsList.Count)].GetUnitPresets().
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
                var isLastLevel = false;
                if (isLastLevel)
                {
                    CalculateGlobalResources(true);
                    GameControlService.ChangeGrid(GameControlService.SelectDungeonGridPrefab);  
                }
                else
                {
                    _runData.SavePlayerUnits(_playerUnits.ToArray());
                    _runData.IsLastPathCellCompleted = true;
                    SaveLoadService.Instance.SaveRunData(_runData);
                    GameControlService.ChangeGrid(GameControlService.PathMapGridPrefab);  
                }
            }
            else if (!HasAliveUnits(_playerUnits))
            {
                CalculateGlobalResources(false);
                GameControlService.ChangeGrid(GameControlService.SelectDungeonGridPrefab);
            }
        }

        private void CalculateGlobalResources(bool isDungeonCompleted)
        {
            
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
        
        private bool HasAliveUnits(List<Unit> units)
        {
            return units.Any(unit => !unit.Stats.IsDead);
        }
    }
}