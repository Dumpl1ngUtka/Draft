namespace Units
{
    public class StatModifier
    {
        public readonly int Type;
        public readonly StatModifierFunction Function;

        public StatModifier(StatModifierType type, StatModifierFunction function)
        {
            Type = (int)type;
            Function = function;
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