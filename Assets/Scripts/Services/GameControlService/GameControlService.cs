using System.Collections.Generic;
using System.Linq;
using Battle.Units;
using DungeonMap;
using Grid;
using Grid.BattleGrid;
using Grid.DraftGrid;
using Grid.PathMapGrid;
using Grid.SelectDungeonGrid;
using PathMap;
using UnityEngine;

namespace Services.GameControlService
{
    public class GameControlService : MonoBehaviour
    {
        [SerializeField] private List<UnitPreset> _enemyPresets;
        [SerializeField] private List<UnitPreset> _enemyPresets2;

        #region Grids
        private DraftGridController _draftGrid;
        private BattleGridController _battleGrid;
        private SelectDungeonGridController _dungeonGrid;
        private PathMapGridController _pathMapGrid;
        private List<GridController> _allGrids;
        private GridController _activeGrid;
        #endregion

        public static GameControlService Instance {get; private set;}
        public DungeonInfo CurrentDungeonInfo { get; private set; }
        public PathCellInfo CurrentPathCellInfo { get; private set; }
        public List<Unit> PlayerUnits { get; private set; }

        public void Init(DraftGridController draftGrid,
            BattleGridController battleGrid,
            SelectDungeonGridController selectDungeonGrid,
            PathMapGridController pathMapGrid)
        {
            Instance = FindFirstObjectByType<GameControlService>();
            
            _draftGrid = draftGrid;
            _battleGrid = battleGrid;
            _dungeonGrid = selectDungeonGrid;
            _pathMapGrid = pathMapGrid;
            
            _draftGrid.Init();
            _battleGrid.Init();
            _dungeonGrid.Init();
            _pathMapGrid.Init();

            _allGrids = new List<GridController>()
            {
                _draftGrid,
                _battleGrid,
                _dungeonGrid,
                _pathMapGrid
            };
        }
        
        private void Start()
        {
            LoadPathMap();
            //LoadDungeonSelectLevel();
        }

        private void LoadPathMap()
        {
            LoadNewGrid(_draftGrid);
        }

        private void FinishPathMap()
        {
            
        }

        #region Dungeon Grid
        private void LoadDungeonSelectLevel()
        {
            LoadNewGrid(_dungeonGrid);
        }
        
        public void FinishDungeonSelectLevel(DungeonInfo info)
        {
            CurrentDungeonInfo = info;
            LoadDraftLevel();
        }
        #endregion

        #region Draft
        private void LoadDraftLevel()
        {
            LoadNewGrid(_draftGrid);
        }
        
        public void FinishDraftLevel(List<Unit> units)
        {
            LoadBattleLevel(units, GetEnemies(_enemyPresets));
        }
        #endregion
        
        #region Battle
        private void LoadBattleLevel(List<Unit> playerUnits, List<Unit> enemyUnits)
        {
            LoadNewGrid(_battleGrid);
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

        private void SetActiveLoadableLevel()
        {
            foreach (var grid in _allGrids)
            {
                grid.SetActive(false);
            }
            _activeGrid.SetActive(true);
            GlobalAnimationService.GlobalAnimationSevice.Instance.PlayRandomTransitionAnimaton(false);
        }

        public void LoadNewGrid(GridController loadableLevel)
        {
            _activeGrid = loadableLevel;
            GlobalAnimationService.GlobalAnimationSevice.Instance.PlayRandomTransitionAnimaton(true, SetActiveLoadableLevel);
        }
        
        private List<Unit> GetEnemies(List<UnitPreset> presets)
        {
            return presets.Select(preset => new Unit(preset)).ToList();
        }
    }
}
