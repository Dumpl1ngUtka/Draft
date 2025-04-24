using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Battle.Units;
using DungeonMap;
using Grid;
using Grid.BattleGrid;
using Grid.DraftGrid;
using Grid.SelectDungeonGrid;
using UnityEngine;

namespace Services.GameControlService
{
    public class GameControlService : MonoBehaviour
    {
        public List<Class> Classes;
        public List<Race> Races;
        [SerializeField] private List<UnitPreset> _enemyPresets;
        [SerializeField] private List<UnitPreset> _enemyPresets2;

        #region Grids
        private DraftGridController _draftGrid;
        private BattleGridController _battleGrid;
        private SelectDungeonGridController _dungeonGrid;
        private List<GridController> _allGrids;
        #endregion

        public static GameControlService Instance {get; private set;}

        public void Init(DraftGridController draftGrid,
            BattleGridController battleGrid,
            SelectDungeonGridController selectDungeonGrid)
        {
            Instance = FindFirstObjectByType<GameControlService>();
            
            _draftGrid = draftGrid;
            _battleGrid = battleGrid;
            _dungeonGrid = selectDungeonGrid;
            
            _draftGrid.Init();
            _battleGrid.Init();
            _dungeonGrid.Init();

            _allGrids = new List<GridController>()
            {
                _draftGrid,
                _battleGrid,
                _dungeonGrid
            };
        }
        
        private void Start()
        {
            LoadDungeonSelectLevel();
        }
        
        #region Dungeon Grid
        private void LoadDungeonSelectLevel()
        {
            foreach (var grid in _allGrids)
            {
                grid.SetActive(false);
            }
            _dungeonGrid.SetActive(true);
        }
        
        public void FinishDungeonSelectLevel(DungeonInfo info)
        {
            Classes = info.Classes;
            Races = info.Races;
            LoadDraftLevel();
        }
        #endregion

        #region Draft
        private void LoadDraftLevel()
        {
            foreach (var grid in _allGrids)
            {
                grid.SetActive(false);
            }
            _draftGrid.SetActive(true);
        }
        
        public void FinishDraftLevel(List<Unit> units)
        {
            LoadBattleLevel(units, GetEnemies(_enemyPresets));
        }
        #endregion
        
        #region Battle
        private void LoadBattleLevel(List<Unit> playerUnits, List<Unit> enemyUnits)
        {
            foreach (var grid in _allGrids)
            {
                grid.SetActive(false);
            }
            _battleGrid.Fill(playerUnits, enemyUnits);
            _battleGrid.SetActive(true);
        }
        
        public void FinishBattleLevel(bool isWin)
        {
            if (isWin)
                PlayerWinOnBattle();
            else
                PlayerDefeatedOnButtle();
        }
        
        private void PlayerWinOnBattle()
        {
            Debug.Log("Win");
        }

        private void PlayerDefeatedOnButtle()
        {
            LoadDraftLevel();
        }
        
        #endregion
        
        private List<Unit> GetEnemies(List<UnitPreset> presets)
        {
            return presets.Select(preset => new Unit(preset)).ToList();
        }
    }
}
