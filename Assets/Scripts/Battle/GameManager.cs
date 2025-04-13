using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Battle.Grid.CardParameter;
using Battle.Units;
using UnityEngine;

namespace Battle
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<UnitPreset> _enemyPresets;
        [SerializeField] private List<UnitPreset> _enemyPresets2;
        [SerializeField] private DraftGrid _draftGrid;
        [SerializeField] private BattleGrid _battleGrid;

        private void Awake()
        {
            _draftGrid.DraftFinished += DraftFinished;
            
            _battleGrid.PlayerWin += PlayerWin;
            _battleGrid.PlayerDefeated += PlayerDefeated;

            LoadDraftLevel();
        }

        private void PlayerWin()
        {
            LoadBattleLevel(_enemyPresets2);
        }

        private void PlayerDefeated()
        {
            LoadDraftLevel();
        }

        private void DraftFinished()
        {
            LoadBattleLevel(_enemyPresets);
        }

        private void LoadDraftLevel()
        {
            _draftGrid.Init();
            
            _draftGrid.SetActive(true);
            _battleGrid.SetActive(false);
        }
        
        private void LoadBattleLevel(List<UnitPreset> presets)
        {
            _battleGrid.Init();
            
            _draftGrid.SetActive(false);
            _battleGrid.Fill(_draftGrid.GetUnits(), GetEnemies(presets));
            _battleGrid.SetActive(true);
        }
        
        private List<Unit> GetEnemies(List<UnitPreset> presets)
        {
            return presets.Select(preset => new Unit(preset)).ToList();
        }
    }
}
