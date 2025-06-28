using System;
using Battle.DamageSystem;
using Units;
using UnityEngine;

namespace Battle.PassiveEffects
{
    [CreateAssetMenu(menuName = "Config/PassiveEffects/ApplyDamage")]
    public class ApplyDamage : PassiveEffect
    {
        [Header("Apply Damage")]
        [SerializeField] private int _baseDamage;
        [SerializeField] private DamageType _damageType;
        [SerializeField] private AttributesType _damageAttribute;
        [SerializeField] private int _damagePerAttribute;
        
        protected override PassiveEffect CreateInstance(UnitStats caster, UnitStats owner)
        {
            var effect = ScriptableObject.CreateInstance<ApplyDamage>();
            effect._baseDamage = _baseDamage;
            effect._damageType = _damageType;
            effect._damageAttribute = _damageAttribute;
            effect._damagePerAttribute = _damagePerAttribute;
            return effect;
        }

        protected override void AddEffect()
        {
            float damage = _baseDamage + _damagePerAttribute *
                Caster.GetAttributeByType(_damageAttribute).Value ;

            if (Owner.Immunities.Contains(_damageType))
                damage = 0;
            if (Owner.Resistances.Contains(_damageType))
                damage /= 2;
            if (Owner.Vulnerability.Contains(_damageType))
                damage *= 2;
            
            Owner.CurrentHealth.AddModifier(new PermanentStatModifier(-(int)damage));
        }

        protected override void TurnEffect()
        {
        }

        protected override void RemoveEffect()
        {
        }
    }
}