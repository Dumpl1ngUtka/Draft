using Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle.PassiveEffects
{
    [CreateAssetMenu(menuName = "Config/PassiveEffects/Healing")]
    public class Healing : PassiveEffect
    {
        [Header("Apply Damage")]
        [SerializeField] private int _baseValue;
        [SerializeField] private AttributesType _attribute;
        [SerializeField] private int _valuePerAttribute;
        
        protected override PassiveEffect CreateInstance(UnitStats caster, UnitStats owner)
        {
            var effect = ScriptableObject.CreateInstance<Healing>();
            effect._baseValue = _baseValue;
            effect._attribute = _attribute;
            effect._valuePerAttribute = _valuePerAttribute;
            return effect;        
        }

        protected override void AddEffect()
        {
            float heal = _baseValue + _valuePerAttribute * Caster.GetAttributeByType(_attribute).Value ;
            Owner.CurrentHealth.AddModifier(new PermanentStatModifier((int)heal));
        }

        protected override void TurnEffect()
        {
        }

        protected override void RemoveEffect()
        {
        }
    }
}