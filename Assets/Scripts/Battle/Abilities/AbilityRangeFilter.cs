using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle.Abilities
{
    [Serializable]
    public class AbilityRangeFilter
    {
        [SerializeField] private bool3x3 _matrix = new bool3x3(false,false,false,
                                                                false,true,false,
                                                                false, false, false);
        [SerializeField, Range(0,2)] private float _lineRange;
        [SerializeField, Range(0,2)] private float _columnRange;
        [SerializeField] private bool _isCornersInclude;
        
        /*public List<GridCell> GetRelevantCells(GridCell caster, GridCell target, List<GridCell> teamCells, List<GridCell> enemyCells)
        {
            var range = new List<GridCell>();
            
            
            var cells = teamCells.Concat(enemyCells).ToList(); 
            foreach (var cell in cells)
            {
                CalculateDistance(cell, target, out int lineDistance, out int columnDistance);
                if (lineDistance <= _lineRange && columnDistance == 0)
                {
                    range.Add(cell);
                    continue;
                }

                if (columnDistance <= _columnRange && lineDistance == 0)
                {
                    range.Add(cell);
                    continue;
                }
                
                if (_isCornersInclude && columnDistance <= _columnRange && lineDistance <= _lineRange)
                {
                    range.Add(cell);
                }
            }
            return range;
        }
        */

        private bool GetMatrixValue(int lineDelta, int columnDelta)
        {
            if (Mathf.Abs(lineDelta) > 1 || Mathf.Abs(columnDelta) > 1)
                return false;

            if (lineDelta == 0 && columnDelta == 0)
                return _matrix.c1.y;
            
            var column = GetColumnByDelta(columnDelta, _matrix);
            return GetLineByDelta(lineDelta, column);
        }

        private bool3 GetColumnByDelta(int columnDelta, bool3x3 matrix)
        {
            return columnDelta switch
            {
                -1 => matrix.c0,
                0 => matrix.c1,
                1 => matrix.c2,
                _ => new bool3(false, false, false)
            };
        }

        private bool GetLineByDelta(int lineDelta, bool3 line)
        {
            return lineDelta switch
            {
                -1 => line.x,
                0 => line.y,
                1 => line.z,
                _ => false
            };
        }

        public List<GridCell> GetRelevantCells(GridCell caster, GridCell target, List<GridCell> teamCells, List<GridCell> enemyCells)
        {
            var range = new List<GridCell>();
            var cells = teamCells.Concat(enemyCells).ToList();
            foreach (var cell in cells)
            {
                CalculateDelta(target, cell, out int lineDelta, out int columnDelta);
                if (GetMatrixValue(lineDelta, columnDelta))
                    range.Add(cell);
            }
            return range;
        }
        
        private void CalculateDelta(GridCell target, GridCell cell, out int lineDelta, out int columnDelta)
        {
            columnDelta = cell.ColumnIndex - target.ColumnIndex;
            if (cell.TeamIndex == target.TeamIndex)
                lineDelta = cell.LineIndex - target.LineIndex;
            else
                lineDelta = -cell.LineIndex - target.LineIndex - 1;
        }
        
        private void CalculateDistance(GridCell cell1, GridCell cell2, out int lineDistance, out int columnDistance)
        {
            columnDistance = Math.Abs(cell1.ColumnIndex - cell2.ColumnIndex);
            if (cell1.TeamIndex == cell2.TeamIndex)
                lineDistance = Math.Abs(cell1.TeamIndex - cell2.TeamIndex);
            else
                lineDistance = cell1.TeamIndex + cell2.TeamIndex + 1;
        }
    }
}