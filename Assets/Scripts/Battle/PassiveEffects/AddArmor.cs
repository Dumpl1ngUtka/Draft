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
        
        public override PassiveEffect GetInstance(Unit caster, Unit owner)
        {
            var effect = ScriptableObject.CreateInstance<AddArmor>();
            effect.Caster = caster;
            effect.Owner = owner;
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