using Battle.Grid;
using UnityEngine;

namespace Battle.Abilities
{
    public abstract class Ability : ScriptableObject 
    {
        public abstract void SetAbility(GridCell casterCell, GridCell target, GridCell[] cells);
    }
}