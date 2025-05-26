using System;
using System.Collections.Generic;
using System.Linq;
using Units;
using Unity.Mathematics;
using UnityEngine;

namespace Abilities
{
    [Serializable]
    public class AbilityRangeFilter
    {
        [SerializeField] private bool3x3 _matrix = new bool3x3(false,false,false,
                                                                false,true,false,
                                                                false, false, false);
        
        public List<Unit> GetRelevantCells(Unit target, List<Unit> teamCells, List<Unit> enemyCells)
        {
            var range = new List<Unit>();
            var cells = teamCells.Concat(enemyCells).ToList();
            foreach (var cell in cells)
            {
                CalculateDelta(target, cell, out int lineDelta, out int columnDelta);
                if (GetMatrixValue(_matrix, lineDelta, columnDelta))
                    range.Add(cell);
            }
            return range;
        }
        
        private bool GetMatrixValue(bool3x3 mask, int lineDelta, int columnDelta)
        {
            if (Mathf.Abs(lineDelta) > 1 || Mathf.Abs(columnDelta) > 1)
                return false;

            if (lineDelta == 0 && columnDelta == 0)
                return mask.c1.y;
            
            var column = GetColumnByDelta(columnDelta, mask);
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
        
        private void CalculateDelta(Unit target, Unit cell, out int lineDelta, out int columnDelta)
        {
            columnDelta = cell.Position.ColumnIndex - target.Position.ColumnIndex;
            if (cell.Position.TeamType == target.Position.TeamType)
                lineDelta = cell.Position.LineIndex - target.Position.LineIndex;
            else
                lineDelta = -cell.Position.LineIndex - target.Position.LineIndex - 1;
        }
    }
}