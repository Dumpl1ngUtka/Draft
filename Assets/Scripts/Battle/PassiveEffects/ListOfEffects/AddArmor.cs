using System.Collections.Generic;
using Battle.DamageSystem;
using Battle.Units;
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
            Owner.Armor.AddFunc(Effect);
        }

        protected override void TurnEffect()
        {
        }

        protected override void RemoveEffect()
        {
            Owner.Armor.RemoveFunc(Effect);
        }

        private float Effect(float value)
        {
            return value + Value;
        }
    }
}