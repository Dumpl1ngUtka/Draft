using System.Collections.Generic;
using Battle.Units.Interactors;
using Grid.Cells;
using UnityEngine;

namespace Battle.UseCardReactions
{
    public abstract class Reaction : ScriptableObject
    {
        [SerializeField] private int _value = 1;
        
        public abstract List<UnitGridCell> GetReactionCells(UnitGridCell caster, List<UnitGridCell> allies);

        public Response TryUseReaction(UnitGridCell caster, List<UnitGridCell> allies)
        {
            foreach (var cell in GetReactionCells(caster, allies))
            {
                cell.Unit.SetDicePower(cell.Unit.DicePower + _value);
            }
            return new Response(true, "ReactionUsed");
        }
    }
}
