using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle.Grid
{
    public class GridVisualizer
    {
        //private readonly List<GridCell> _playerCells;
        //private readonly List<GridCell> _enemyCells;
        private readonly List<GridCell> _allCells = new List<GridCell>();

        /*public GridVisualizer(List<GridCell> playerCells, List<GridCell> enemyCells)
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
        }*/

        public void AddCells(List<GridCell> cells)
        {
            _allCells.AddRange(cells);
        }
        
        public void SetSizeFor(float size, List<GridCell> cells, bool instantly = false)
        {
            foreach (var cell in cells)
                cell.Renderer.SetSize(size, instantly);
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
                if (cell.Unit.IsReady)
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
            var ablility = caster.Unit.CurrentAbility;
            foreach (var cell in _allCells)
            {
                if (!ablility.IsRightTarget(caster, cell))
                    continue;
                
                var probability = ablility.GetHitProbability(caster, cell);
                cell.Renderer.SetOverText(true, (probability).ToString("0%"));
            }
        }

        public void SetOverPanel(GridCell cell, Color color)
        {
            cell.Renderer.SetOverPanel(true, color);
        }

        public void ResetOverPanels()
        {
            foreach (var cell in _allCells)
            {
                cell.Renderer.SetOverPanel(false);
            }
        }
        
        public void HideOverText()
        {
            foreach (var cell in _allCells)
            {
                cell.Renderer.SetOverText(false);
            }
        }

        public void PlayEffect(Grid grid)
        {
            grid.StartCoroutine(EnableEffect());
        }

        private IEnumerator EnableEffect()
        {
            SetSizeFor(0, _allCells, true);
            var delay = new WaitForSeconds(0.5f);
            var iterationCount = _allCells.Max(cell => cell.ColumnIndex);
            for (int i = 0; i <= iterationCount; i++)
            {
                SetSizeFor(1, _allCells.Where(x => x.ColumnIndex == i).ToList());     
                yield return delay;
            }
        }
    }
}