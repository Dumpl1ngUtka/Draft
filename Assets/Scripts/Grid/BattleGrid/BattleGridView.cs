using System.Collections.Generic;
using Grid.Cells;
using Units;

namespace Grid.BattleGrid
{
    public class BattleGridView : GridView
    {
        public void GetCells(out List<UnitGridCell> playerCells, out List<UnitGridCell> enemyCells)
        {
            playerCells = GetUnitsCellsByTeam(TeamType.Player);
            enemyCells = GetUnitsCellsByTeam(TeamType.Enemy);
        }
    }
}