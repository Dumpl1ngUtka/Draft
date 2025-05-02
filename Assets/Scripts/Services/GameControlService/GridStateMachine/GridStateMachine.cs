using System.Collections.Generic;
using Grid;
using Grid.BattleGrid;
using Grid.DraftGrid;
using Grid.PathMapGrid;
using Grid.SelectDungeonGrid;

namespace Services.GameControlService.GridStateMachine
{
    public class GridStateMachine
    {
        #region Grids
        public DraftGridController DraftGrid {get; private set;}
        public  BattleGridController BattleGrid {get; private set;}
        public  SelectDungeonGridController DungeonGrid {get; private set;}
        public  PathMapGridController PathMapGrid {get; private set;}
        private List<GridController> _allGrids;
        private GridController _activeGrid;
        #endregion
        
        public GridStateMachine(DraftGridController draftGrid,
            BattleGridController battleGrid,
            SelectDungeonGridController selectDungeonGrid,
            PathMapGridController pathMapGrid)
        {
            DraftGrid = draftGrid;
            BattleGrid = battleGrid;
            DungeonGrid = selectDungeonGrid;
            PathMapGrid = pathMapGrid;
            
            DraftGrid.Init(this);
            BattleGrid.Init(this);
            DungeonGrid.Init(this);
            PathMapGrid.Init(this);

            _allGrids = new List<GridController>()
            {
                DraftGrid,
                BattleGrid,
                DungeonGrid,
                PathMapGrid
            };
        }

        public void ChangeGrid(GridController newGrid)
        {
            _activeGrid?.OnExit();
            _activeGrid = newGrid;
            GlobalAnimationService.GlobalAnimationSevice.Instance.PlayRandomTransitionAnimaton(true, SetActiveLoadableLevel);
        }
        
        private void SetActiveLoadableLevel()
        {
            foreach (var grid in _allGrids)
            {
                grid.SetActive(false);
            }
            _activeGrid.SetActive(true);
            _activeGrid.OnEnter();
            GlobalAnimationService.GlobalAnimationSevice.Instance.PlayRandomTransitionAnimaton(false);
        }
    }
}