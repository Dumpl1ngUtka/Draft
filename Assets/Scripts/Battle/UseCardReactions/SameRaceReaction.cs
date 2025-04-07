using System.Collections.Generic;
using System.Linq;
using Battle.Grid;
using Battle.Units;
using Battle.Units.Interactors;
using UnityEngine;

namespace Battle.UseCardReactions
{
    [CreateAssetMenu(menuName = "Config/Reactions/SameRaceReaction")]

    public class SameRaceReaction : Reaction
    {
        [SerializeField] private int _value;
        
        public override Response TryUseReaction(GridCell caster, List<GridCell> allies)
        {
            var casterRace = caster.Unit.Race;
            var upgradableCells = allies.Where(x => x.Unit.Race == casterRace).ToList();
            upgradableCells = upgradableCells.Distinct().ToList();;
            foreach (var cell in upgradableCells)
            {
                cell.Unit.SetPower(cell.Unit.DicePower + _value);
            }
            return new Response(true, "ReactionUsed");
        }
    }
}