using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Battle.Units;
using UnityEngine;

namespace Battle.UseCardReactions
{
    [CreateAssetMenu(menuName = "Config/Reactions/SameRaceReaction")]

    public class SameRaceReaction : Reaction
    {
        [SerializeField] private int _value;
        
        public override void SetReaction(GridCell casterCell, GridCell[] cells)
        {
            var casterRace = (casterCell.Unit as PlayerUnit)?.Race;
            var upgradableCells = cells.Where(x => (x.Unit as PlayerUnit)?.Race == casterRace).ToList();
            upgradableCells = upgradableCells.GroupBy(x => x.Index).Select(y => y.First()).ToList();
            foreach (var cell in upgradableCells)
            {
                cell.SetPower(cell.DicePower + _value);
            }
        }
    }
}