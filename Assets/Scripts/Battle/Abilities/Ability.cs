using System.Collections.Generic;
using Battle.Units;
using Battle.Units.Interactors;
using UnityEngine;

namespace Battle.Abilities
{
    public abstract class Ability : ScriptableObject 
    {
        public string Name;
        public Sprite Icon;
        
        public abstract Response TryUseAbility(Unit caster, Unit target, List<Unit> allies, List<Unit> enemies);
        
        public abstract Unit GetPreferredTarget(List<Unit> potentialTargets);
    }
}