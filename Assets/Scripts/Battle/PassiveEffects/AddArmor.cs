using Battle.Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle.PassiveEffects
{    
    [CreateAssetMenu(menuName = "Config/PassiveEffects/AddArmor")]
    public class AddArmor : PassiveEffect
    {
        [Header("Add Armor")]
        public int Value;
        
        protected override PassiveEffect CreateInstance(Unit caster, Unit owner)
        {
            var effect = ScriptableObject.CreateInstance<AddArmor>();
            effect.Value = Value;
            return effect;
        }

        protected override void AddEffect()
        {
            Owner.Health.AddArmor(Value);
        }

        protected override void TurnEffect()
        {
        }

        protected override void RemoveEffect()
        {
            //Owner.Health.RemoveArmor(Value);
        }
    }
}