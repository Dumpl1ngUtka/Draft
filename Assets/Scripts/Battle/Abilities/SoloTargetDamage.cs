using Battle.DamageSystem;
using Battle.Grid;
using UnityEngine;

namespace Battle.Abilities
{
    [CreateAssetMenu(menuName = "Config/Ability/SoloTargetDamage")]
    public class SoloTargetDamage : Ability
    {
        [SerializeField] private int _baseDamage;
        [SerializeField] private int _damagePerStrength;
        [SerializeField] private int _damagePerIntelligence;
        [SerializeField] private DamageType _damageType;
        
        public override void SetAbility(GridCell casterCell, GridCell target, GridCell[] cells)
        {
            var damageValue = _baseDamage 
                              + casterCell.Unit.Attributes.Strength * _damagePerStrength 
                              + casterCell.Unit.Attributes.Intelligence * _damagePerIntelligence; 
            var damage = new Damage(damageValue, _damageType);
            target.Health.ApplyDamage(damage);
        }
    }
}