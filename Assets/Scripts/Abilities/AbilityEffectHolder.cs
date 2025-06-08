using System;
using Battle.PassiveEffects;

namespace Abilities
{
    [Serializable]
    public class AbilityEffectHolder
    {
        public PassiveEffect Effect;
        public HitProbabilityCalculator Probability;
    }
}