using System;
using Battle.PassiveEffects;

namespace Units
{
    public class StatModifier
    {
        public readonly int Type;
        public readonly StatModifierFunction Function;
        public Action<StatModifier> ModifireRemoved;

        public StatModifier(StatModifierType type, StatModifierFunction function, PassiveEffect holder = null)
        {
            Type = (int)type;
            Function = function;
            if (holder != null) 
                holder.EffectRemoved += () => ModifireRemoved?.Invoke(this);
        }
    }
    
    public class PermanentStatModifier
    {
        public readonly int Type;
        public readonly StatModifierFunction Function;

        public PermanentStatModifier(StatModifierFunction function)
        {
            Type = (int)StatModifierType.PermanentAdditiveValue;
            Function = function;
        }
        
        public PermanentStatModifier(int additionValue)
        {
            Type = (int)StatModifierType.PermanentAdditiveValue;
            Function = (value => value + additionValue);
        }
    }
}