using Units;
using UnityEngine;

namespace Battle.PassiveEffects
{
    [CreateAssetMenu(menuName = "Config/PassiveEffects/EnergyReduce")]
    public class EnergyReduce : PassiveEffect
    {
        protected override PassiveEffect CreateInstance(UnitStats caster, UnitStats owner)
        {
            var instance = ScriptableObject.CreateInstance<EnergyReduce>();
            return instance;
        }

        protected override void AddEffect()
        {
            Caster.Energy.AddModifier(new StatModifier(StatModifierType.AdditiveValue, value => value - 1, this));
        }

        protected override void TurnEffect()
        {
            ;
        }

        protected override void RemoveEffect()
        {
            ;
        }
    }
}