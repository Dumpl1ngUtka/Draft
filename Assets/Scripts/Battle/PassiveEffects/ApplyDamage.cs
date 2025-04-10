using Battle.DamageSystem;
using Battle.Units;
using UnityEngine;

namespace Battle.PassiveEffects
{
    [CreateAssetMenu(menuName = "Config/PassiveEffects/ApplyDamage")]
    public class ApplyDamage : PassiveEffect
    {
        [Header("Apply Damage")]
        public int _baseDamage;
        public  DamageType _damageType;
        public  AttributesType _damageAttribute;
        public  int _damagePerAttribute;
        
        
        public override PassiveEffect GetInstance(Unit caster, Unit owner)
        {
            var effect = ScriptableObject.CreateInstance<ApplyDamage>();
            effect._damagePerAttribute = _damagePerAttribute;
            effect._damageAttribute = _damageAttribute;
            effect._baseDamage = _baseDamage;
            effect._damageType = _damageType;
            effect.Caster = caster;
            effect.Owner = owner;
            return effect;
        }

        protected override void AddEffect()
        {
            var damageValue = _baseDamage + Caster.Attributes.GetAttributeValueByType(_damageAttribute) * _damagePerAttribute;
            var damage = new Damage(damageValue, _damageType);
            Owner.Health.ApplyDamage(damage);
        }

        protected override void TurnEffect()
        {
        }

        protected override void RemoveEffect()
        {
        }
    }
}