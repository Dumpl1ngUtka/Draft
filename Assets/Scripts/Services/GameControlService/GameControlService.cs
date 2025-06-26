using DungeonMap;
using Grid;
using Grid.BattleGrid;
using Grid.DraftGrid;
using Grid.PathMapGrid;
using Grid.SelectDungeonGrid;
using UnityEngine;

namespace Services.GameControlService
{
    public class GameControlService : MonoBehaviour
    {
        public static GameControlService Instance {get; private set;}

        public DraftGridController DraftGridPrefab;
        public BattleGridController BattleGridPrefab;
        public SelectDungeonGridController SelectDungeonGridPrefab;
        public PathMapGridController PathMapGridPrefab;
        
        private Transform _gridContainer;
        private GridController _activeGrid;
        private GridController _nextGridPrefab;

        public void Init(Transform gridContainer,
            DraftGridController draftGrid,
            BattleGridController battleGrid,
            SelectDungeonGridController selectDungeonGrid,
            PathMapGridController pathMapGrid)
        {
            Instance = FindFirstObjectByType<GameControlService>();
            
            _gridContainer = gridContainer;
            
            DraftGridPrefab = draftGrid;
            BattleGridPrefab = battleGrid;
            SelectDungeonGridPrefab = selectDungeonGrid;
            PathMapGridPrefab = pathMapGrid;
        }

        public void ChangeGrid(GridController prefab)
        {
            _nextGridPrefab = prefab;
            _activeGrid?.Exit();
            Invoke(nameof(LoadNewGrid), 1f);
        }
        
        private void LoadNewGrid()
        {
            Destroy(_activeGrid?.gameObject);
            var instance = Instantiate(_nextGridPrefab, _gridContainer);
            _activeGrid = instance;
            _activeGrid.Enter();
        }
    }
}
