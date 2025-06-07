using Units;
using UnityEngine;

namespace Battle.PassiveEffects
{    
    [CreateAssetMenu(menuName = "Config/PassiveEffects/AddArmor")]
    public class AddArmor : PassiveEffect
    {
        [Header("Add Armor")]
        public int Value;
        
        protected override PassiveEffect CreateInstance(UnitStats caster, UnitStats owner)
        {
            var effect = ScriptableObject.CreateInstance<AddArmor>();
            effect.Value = Value;
            return effect;
        }

        protected override void AddEffect()
        {
            Owner.Armor.AddModifier(new StatModifier(StatModifierType.AdditiveValue, value => value + Value, this));
        }

        protected override void TurnEffect()
        {
        }

        protected override void RemoveEffect()
        {
        }
    }
}