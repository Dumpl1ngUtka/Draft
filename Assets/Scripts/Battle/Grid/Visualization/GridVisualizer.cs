using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Grid.Cells;
using UnityEngine;

namespace Battle.Grid.Visualization
{
    public class GridVisualizer
    {
        private readonly List<UnitGridCell> _allCells = new List<UnitGridCell>();
        
        public void AddCells(List<UnitGridCell> cells)
        {
            _allCells.AddRange(cells);
        }
        
        public void SetSizeFor(float size, List<UnitGridCell> cells, bool instantly = false)
        {
            foreach (var cell in cells)
                cell.Renderer.SetSize(size, instantly);
        }

        public void SetSizeFor(float size, UnitGridCell cell, bool instantly = false)
        {
            cell.Renderer.SetSize(size, instantly);
        }

        public void ResetSize()
        {
            foreach (var cell in _allCells)
                cell.Renderer.SetSize(1f);
        }

        public void RenderHitProbabilityForAll(UnitGridCell caster)
        {
            /*var ablility = caster.Unit.CurrentAbility;
            foreach (var cell in _allCells)
            {
                if (!ablility.IsRightTarget(caster, cell))
                    continue;
                
                var probability = ablility.GetHitProbability(caster, cell);
                cell.Renderer.SetOverText((probability).ToString("0%"));
            }*/
        }
        
        public void HideOverText()
        {
            foreach (var cell in _allCells)
            {
                cell.Renderer.SetOverText("");
            }
        }

        /*public void PlayEffect(Grid grid)
        {
            grid.StartCoroutine(EnableEffect());
        }*/

        private IEnumerator EnableEffect()
        {
            SetSizeFor(0, _allCells, true);
            var delay = new WaitForSeconds(0.5f);
            var iterationCount = _allCells.Max(cell => cell.Position.ColumnIndex);
            for (int i = 0; i <= iterationCount; i++)
            {
                SetSizeFor(1, _allCells.Where(x => x.Position.ColumnIndex == i).ToList());     
                yield return delay;
            }
        }
    }
}