using System.Collections.Generic;
using System.Linq;
using Battle.DamageSystem;
using Battle.Units;
using Battle.Units.Interactors;
using UnityEngine;

namespace Battle.Abilities
{
    [CreateAssetMenu(menuName = "Config/Ability/SoloTargetDamage")]
    public class SoloTargetDamage : Ability
    {
        [Header("Solo Target Damage")]
        [SerializeField] private int _baseDamage;
        [SerializeField] private int _damagePerStrength;
        [SerializeField] private int _damagePerIntelligence;
        [SerializeField] private DamageType _damageType;

        public override float GetHitProbability(Unit caster, Unit target)
        {
            return 1f;
        }

        public override Response TryUseAbility(Unit caster, Unit target, List<Unit> allies, List<Unit> enemies)
        {
            if (caster.IsDead)
                return new Response(false, "unit_dead_error");
                
            if (target.TeamIndex == caster.TeamIndex)
                return new Response(false, "teammete_error");
            
            if (target.IsDead)
                return new Response(false, "target_dead_error");
            
            var damageValue = _baseDamage 
                              + caster.Attributes.Strength * _damagePerStrength 
                              + caster.Attributes.Intelligence * _damagePerIntelligence; 
            var damage = new Damage(damageValue, _damageType);
            target.Health.ApplyDamage(damage);
            return new Response(true, "Damage used");
        }

        public override Unit GetPreferredTarget(List<Unit> potentialTargets)
        {
            return potentialTargets[Random.Range(0, potentialTargets.Count)];
        }
    }
}