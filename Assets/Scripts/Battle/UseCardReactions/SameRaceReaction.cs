using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Battle.Units;
using Battle.Units.Interactors;
using Grid.Cells;
using UnityEngine;

namespace Battle.UseCardReactions
{
    [CreateAssetMenu(menuName = "Config/Reactions/SameRaceReaction")]

    public class SameRaceReaction : Reaction
    {
        public override List<UnitGridCell> GetReactionCells(UnitGridCell caster, List<UnitGridCell> allies)
        {
            var casterRace = caster.Unit.Race;
            var upgradableCells = allies.Where(x => x.Unit.Race == casterRace).ToList();
            return upgradableCells.Distinct().ToList();
        }
    }
}