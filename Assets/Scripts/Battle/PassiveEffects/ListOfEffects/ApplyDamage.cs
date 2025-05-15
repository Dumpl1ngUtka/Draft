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

        [HideInInspector] public int DamageValue;
        public DamageType DamageType => _damageType;
        
        protected override PassiveEffect CreateInstance(UnitStats caster, UnitStats owner)
        {
            var effect = ScriptableObject.CreateInstance<ApplyDamage>();
            effect._baseDamage = _baseDamage;
            effect._damageType = _damageType;
            effect._damageAttribute = _damageAttribute;
            effect._damagePerAttribute = _damagePerAttribute;
            effect.DamageValue = effect._baseDamage + caster.StrengthAttribute.Value * effect._damagePerAttribute;
            return effect;
        }

        protected override void AddEffect()
        {
            Owner.CurrentHealth -= DamageValue;
        }

        protected override void TurnEffect()
        {
        }

        protected override void RemoveEffect()
        {
        }
    }
}