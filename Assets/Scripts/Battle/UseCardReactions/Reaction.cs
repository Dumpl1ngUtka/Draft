using System.Collections.Generic;
using Battle.Grid;
using Battle.Units.Interactors;
using UnityEngine;

namespace Battle.UseCardReactions
{
    public abstract class Reaction : ScriptableObject
    {
        [SerializeField] private int _value = 1;
        
        public abstract List<GridCell> GetReactionCells(GridCell caster, List<GridCell> allies);

        public Response TryUseReaction(GridCell caster, List<GridCell> allies)
        {
            foreach (var cell in GetReactionCells(caster, allies))
            {
                cell.Unit.SetDicePower(cell.Unit.DicePower + _value);
            }
            return new Response(true, "ReactionUsed");
        }
    }
}
