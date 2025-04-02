using Battle.Grid;
using UnityEngine;

namespace Battle.UseCardReactions
{
    public abstract class Reaction : ScriptableObject
    {
        public abstract void SetReaction(GridCell casterCell, GridCell[] cells);
    }
}
