using System.Collections.Generic;
using Battle.Grid;
using Battle.Units.Interactors;
using UnityEngine;

namespace Battle.UseCardReactions
{
    public abstract class Reaction : ScriptableObject
    {
        public abstract Response TryUseReaction(GridCell caster, List<GridCell> allies);
    }
}
