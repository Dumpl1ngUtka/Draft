using System.Collections.Generic;
using System.Linq;
using Battle.Grid.Visualization;
using Battle.Units;
using Grid.Cells;
using Units;

namespace Grid.BattleGrid
{
    public class BattleGridView : GridView
    {
        public void Fill(List<Unit> playerUnits, List<Unit> enemyUnits, 
            out List<UnitGridCell> playerCells, out List<UnitGridCell> enemyCells)
        {
            playerCells = GetUnitsCellsByTeam(TeamType.Player);
            FillCells(playerUnits, playerCells);
            
            enemyCells = GetUnitsCellsByTeam(TeamType.Enemy);
            FillCells(enemyUnits, enemyCells);
        }

        private void FillCells(List<Unit> units, List<UnitGridCell> cells)
        {
            var index = 0;
            foreach (var cell in cells)
            {
                cell.AddUnit(units[index++]);
            }
        }
    }
}