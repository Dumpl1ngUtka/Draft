using System.Collections.Generic;
using System.Linq;

namespace Battle.Grid
{
    public class GridVisualizer
    {
        private readonly List<GridCell> _playerCells;
        private readonly List<GridCell> _enemyCells;
        private readonly List<GridCell> _allCells;

        public GridVisualizer(List<GridCell> playerCells, List<GridCell> enemyCells)
        {
            _allCells = playerCells.Concat(enemyCells).ToList();
            _playerCells = playerCells;
            _enemyCells = enemyCells;
        }
        
        public GridVisualizer(List<GridCell> playerCells)
        {
            _allCells = playerCells;
            _playerCells = playerCells;
            _enemyCells = new List<GridCell>();
        }
        
        public void SetSizeFor(float size, List<GridCell> cells)
        {
            foreach (var cell in cells)
            {
                cell.Renderer.SetSize(size);
            }
        }

        public void ResetSize()
        {
            foreach (var cell in _allCells)
                cell.Renderer.SetSize(1f);
        }

        public void RenderDiceAdditionValueFor(int value, List<GridCell> cells)
        {
            foreach (var cell in cells)
            {
                cell.Renderer.RenderDiceAdditionValue(value);
            }
        }
        
        public void ResetDiceAdditionValue()
        {
            foreach (var cell in _allCells)
                cell.Renderer.RenderDiceAdditionValue(0);
        }

        public void RenderHitProbability(GridCell caster)
        {
            foreach (var cell in _allCells)
            {
                var ablility = cell.Unit.CurrentAbility;
                if (!ablility.IsRightTarget(caster, cell))
                    continue;
                
                var probability = ablility.GetHitProbability(caster, cell);
                cell.Renderer.SetOverText(true, (probability).ToString("0%"));
            }
        }
        
        public void HideOverText()
        {
            foreach (var cell in _allCells)
            {
                cell.Renderer.SetOverText(false);
            }
        }
    }
}