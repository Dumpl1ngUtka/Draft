using Battle.Grid;
using UnityEngine;

namespace Battle.Abilities
{
    public abstract class Ability : ScriptableObject 
    {
        public string Name;
        public Sprite Icon;
        
        public abstract bool TryUseAbility(GridCell casterCell, GridCell target, GridCell[] cells);
    }
}