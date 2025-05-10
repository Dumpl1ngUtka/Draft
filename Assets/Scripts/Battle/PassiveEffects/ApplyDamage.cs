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
        
        protected override PassiveEffect CreateInstance(Unit caster, Unit owner)
        {
            return ScriptableObject.CreateInstance<ApplyDamage>();
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