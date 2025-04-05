using Battle.DamageSystem;
using Battle.Grid;
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
        
        public override bool TryUseAbility(GridCell casterCell, GridCell target, GridCell[] cells)
        {
            if (target.TeamIndex == casterCell.TeamIndex)
                return false;
            
            var damageValue = _baseDamage 
                              + casterCell.Unit.Attributes.Strength * _damagePerStrength 
                              + casterCell.Unit.Attributes.Intelligence * _damagePerIntelligence; 
            var damage = new Damage(damageValue, _damageType);
            target.Health.ApplyDamage(damage);
            return true;
        }
    }
}