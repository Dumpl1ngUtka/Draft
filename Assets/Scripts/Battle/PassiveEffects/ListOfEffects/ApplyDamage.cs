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
        [Header("Periodic Damage")]
        [SerializeField] private int _periodicBaseDamage;
        [SerializeField] private int _periodicDamagePerAttribute;
        
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

            var attackMod = ((float)Caster.AttackRIV.GetStatBy(_damageType).Value / 100) + 1;
            var defenceMod = ((float)Owner.DefenceRIV.GetStatBy(_damageType).Value / 100) + 1;
            
            damage = (damage * attackMod) * defenceMod;   
            
            Owner.CurrentHealth.AddModifier(new PermanentStatModifier(-(int)damage));
        }

        protected override void TurnEffect()
        {
            float damage = _periodicBaseDamage + _damagePerAttribute *
                Caster.GetAttributeByType(_damageAttribute).Value;

            var attackMod = ((float)Caster.AttackRIV.GetStatBy(_damageType).Value / 100) + 1;
            var defenceMod = ((float)Owner.DefenceRIV.GetStatBy(_damageType).Value / 100) + 1;
            
            damage = (damage * attackMod) * defenceMod;   
            
            Owner.CurrentHealth.AddModifier(new PermanentStatModifier(-(int)damage));
        }

        protected override void RemoveEffect()
        {
        }
    }
}