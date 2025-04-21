using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Battle.Units;
using UnityEngine;

namespace Services.GameControlService
{
    public class GameControlService : MonoBehaviour
    {
        [SerializeField] private List<UnitPreset> _enemyPresets;
        [SerializeField] private List<UnitPreset> _enemyPresets2;
        private DraftGrid _draftGrid;
        private BattleGrid _battleGrid;

        public GameControlService Instance {get; private set;}

        public void Init(DraftGrid draftGrid, BattleGrid battleGrid)
        {
            Instance = FindFirstObjectByType<GameControlService>();
            _draftGrid = draftGrid;
            _battleGrid = battleGrid;
        }
        
        private void Awake()
        {
            _draftGrid.DraftFinished += DraftFinished;
            
            _battleGrid.PlayerWin += PlayerWin;
            _battleGrid.PlayerDefeated += PlayerDefeated;

            LoadDraftLevel();
        }

        public void StartDraft(List<UnitPreset> enemyPresets)
        {
            
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
