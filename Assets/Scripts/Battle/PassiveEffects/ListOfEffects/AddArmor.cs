using Units;
using UnityEngine;

namespace Battle.PassiveEffects
{    
    [CreateAssetMenu(menuName = "Config/PassiveEffects/AddArmor")]
    public class AddArmor : PassiveEffect
    {
        [Header("Add Armor")]
        public int Value;
        private StatModifier _statModifier;
        
        protected override PassiveEffect CreateInstance(UnitStats caster, UnitStats owner)
        {
            var effect = ScriptableObject.CreateInstance<AddArmor>();
            effect.Value = Value;
            effect._statModifier = new StatModifier(StatModifierType.AdditiveValue, Effect);
            return effect;
        }

        protected override void AddEffect()
        {
            Owner.Armor.AddModifier(_statModifier);
        }

        protected override void TurnEffect()
        {
        }

        protected override void RemoveEffect()
        {
            Owner.Armor.RemoveModifier(_statModifier);
        }

        private float Effect(float value)
        {
            return value + Value;
        }
    }
}