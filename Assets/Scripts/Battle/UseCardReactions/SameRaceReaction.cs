using System.Collections.Generic;
using System.Linq;
using Units;
using UnityEngine;

namespace Battle.UseCardReactions
{
    [CreateAssetMenu(menuName = "Config/Reactions/SameRaceReaction")]

    public class SameRaceReaction : Reaction
    {
        public override List<Unit> GetReactionCells(Unit caster, List<Unit> allies)
        {
            var casterRace = caster.Race;
            var upgradableUnits = allies.Where(x => x.Race == casterRace).ToList();
            return upgradableUnits.Distinct().ToList();
        }
    }
}