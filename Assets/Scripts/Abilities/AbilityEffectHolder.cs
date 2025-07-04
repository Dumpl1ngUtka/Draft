using System;
using Battle.PassiveEffects;
using UnityEngine;

namespace Abilities
{
    [Serializable]
    public class AbilityEffectHolder
    {
        public PassiveEffect Effect;
        public int TurnCount;
        public bool IsCasterEffect = false;
        [HideInInspector] public HitProbabilityCalculator Probability;
    }
}