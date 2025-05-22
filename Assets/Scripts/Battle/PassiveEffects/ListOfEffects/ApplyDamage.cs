using System;
using Battle.DamageSystem;
using Battle.Units;
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
            var damage = _baseDamage + _damagePerAttribute *
                Owner.GetAttributeByType(_damageAttribute).Value ;
            
            var armorValue = Owner.Armor.Value;
            var damageToArmorMultiplayer = _damageType == DamageType.Acid ? 2 : 1;
            
            var damageToArmor = Math.Min(armorValue, damage * damageToArmorMultiplayer);
            Owner.Armor.AddModifier(new PermanentStatModifier(-damageToArmor));

            var damageToHealth = damage - damageToArmor / damageToArmorMultiplayer;
            Owner.CurrentHealth.AddModifier(new PermanentStatModifier(-damageToHealth));
        }

        protected override void TurnEffect()
        {
        }

        protected override void RemoveEffect()
        {
        }
    }
}