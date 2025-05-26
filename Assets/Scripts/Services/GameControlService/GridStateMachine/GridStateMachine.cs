using Grid;
using Services.GlobalAnimation;

namespace Services.GameControlService.GridStateMachine
{
    public class GridStateMachine
    {
        private GridController _activeGrid;
        private GridController _nextGrid;

        public void ChangeGrid(GridController newGrid)
        {
            _nextGrid = newGrid;
            GlobalAnimationSevice.Instance.PlayRandomTransitionAnimaton(true, LoadNewGrid);
        }
        
        private void LoadNewGrid()
        {
            _activeGrid?.Exit();
            _activeGrid = _nextGrid;
            _activeGrid.Enter();
            GlobalAnimationSevice.Instance.PlayRandomTransitionAnimaton(false);
        }
    }
}