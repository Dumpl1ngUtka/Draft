using System.Collections.Generic;
using Battle.Units;
using Battle.Units.Interactors;
using UnityEngine;

namespace Battle.Abilities
{
    public class AddArmor : Ability
    {
        [SerializeField] private int _additionalArmor;
        public override float GetHitProbability(Unit caster, Unit target)
        {
            return 1f;
        }

        public override Response TryUseAbility(Unit caster, Unit target, List<Unit> allies, List<Unit> enemies)
        {
            if (caster.IsDead)
                return new Response(false, "unit_dead_error");
                
            if (target.TeamIndex != caster.TeamIndex)
                return new Response(false, "team_index_mismatch_error");
            
            if (target.IsDead)
                return new Response(false, "target_dead_error");
            
            target.Health.AddArmor(_additionalArmor);
            return new Response(true, "Add armor");
        }

        public override Unit GetPreferredTarget(List<Unit> potentialTargets)
        {
            return potentialTargets[Random.Range(0, potentialTargets.Count)];
        }
    }
}