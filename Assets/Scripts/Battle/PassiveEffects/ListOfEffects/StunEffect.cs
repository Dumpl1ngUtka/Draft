using Units;
using UnityEngine;

namespace Battle.PassiveEffects
{
    [CreateAssetMenu(menuName = "Config/PassiveEffects/StunEffect")]
    public class StunEffect: PassiveEffect
    {
        protected override PassiveEffect CreateInstance(UnitStats caster, UnitStats owner)
        {
            return ScriptableObject.CreateInstance<StunEffect>();
        }

        protected override void AddEffect()
        {
            Owner.Energy.AddModifier(new StatModifier(StatModifierType.AdditiveValue, value => 0, this));
        }

        protected override void TurnEffect()
        {
        }

        protected override void RemoveEffect()
        {
        }
    }
}