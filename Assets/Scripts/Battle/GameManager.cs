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
        [SerializeField] private DraftGrid _draftGrid;
        [SerializeField] private BattleGrid _battleGrid;
        private List<Unit> _playerUnits = new List<Unit>();

        private void Awake()
        {
            _draftGrid.Init();
            _battleGrid.Init();
            _battleGrid.gameObject.SetActive(false);
            _draftGrid.gameObject.SetActive(true);
            
            _draftGrid.DraftFinished += DraftFinished;
        }

        private void DraftFinished()
        {
            _draftGrid.gameObject.SetActive(false);
            _playerUnits = _draftGrid.GetUnits();
            _battleGrid.Fill(_playerUnits, GetEnemies(_enemyPresets));
            _battleGrid.gameObject.SetActive(true);
        }
        
        private List<Unit> GetEnemies(List<UnitPreset> presets)
        {
            return presets.Select(preset => new Unit(preset)).ToList();
        }
    }
}
